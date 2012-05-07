﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Avenue.ApplicationBus
{
        public class InternalBus : ICommandSender, IEventPublisher
        {
            public void ResetRoutes()
            {
                foreach (var cur in _routes)
                {
                    _routes.Remove(cur.Key);
                }
            }

            private readonly Dictionary<Type, List<Type>> _routes = new Dictionary<Type, List<Type>>();

            public void RegisterCommandHandler<C, H>()
                where C : Command
                where H : HandlesCommand<C>
            {
                RegisterHandler<C, H>();
            }

            public void RegisterEventHandler<E, H>()
                where E : Event
                where H : HandlesEvent<E>
            {
                RegisterHandler<E, H>();
            }

            public void RegisterHandler(Type messageType, Type handlerType)
            {

                if (!(messageType.IsSubclassOf(typeof(Command)) || messageType.IsSubclassOf(typeof(Event))))
                {
                    throw new InvalidOperationException("cannot add more than one command handler for a command");

                }
                List<Type> handlers;
                if (!_routes.TryGetValue(messageType, out handlers))
                {
                    handlers = new List<Type>();
                    _routes.Add(messageType, handlers);
                }

                if (messageType.IsSubclassOf(typeof(Command)))
                {
                    //if (!handlers.Contains())

                    if (handlers.Count > 0)
                    {
                        throw new InvalidOperationException("cannot add more than one command handler for a command");
                    }
                    else
                    {
                        handlers.Add(handlerType);
                    }
                }
                else
                {
                    if (handlers.Contains(handlerType))
                    {
                        throw new InvalidOperationException("cannot add the same handler for an event multiple times");
                    }
                    handlers.Add(handlerType);
                }

            }

            private void RegisterHandler<M, H>()
            {
                List<Type> handlers;
                if (!_routes.TryGetValue(typeof(M), out handlers))
                {
                    handlers = new List<Type>();
                    _routes.Add(typeof(M), handlers);
                }

                if (typeof(M).IsSubclassOf(typeof(Command)))
                {
                    //if (!handlers.Contains())

                    if (handlers.Count > 0)
                    {
                        throw new InvalidOperationException("cannot add more than one command handler for a command");
                    }
                    else
                    {
                        handlers.Add(typeof(H));
                    }
                }
                else
                {
                    if (handlers.Contains(typeof(H)))
                    {
                        throw new InvalidOperationException("cannot add the same handler for an event multiple times");
                    }
                    handlers.Add(typeof(H));
                }

            }

            public void Send<T>(T command) where T : Command
            {
                List<Type> handlers;
                if (_routes.TryGetValue(typeof(T), out handlers))
                {
                    if (handlers.Count != 1) throw new InvalidOperationException("cannot send to more than one handler");

                    var handler = CreateInstance<HandlesCommand<T>>(handlers[0]);
                    handler.Handle(command);

                }
                else
                {
                    throw new InvalidOperationException("no handler registered");
                }
            }

            public void Publish<T>(T @event) where T : Event
            {
                List<Type> handlers;
                if (!_routes.TryGetValue(@event.GetType(), out handlers)) return;
                foreach (var handler in handlers)
                {
                    //dispatch on thread pool for added awesomeness

                    var handlerInstance = CreateInstance<HandlesEvent<T>>(handler);
                    handlerInstance.Handle(@event);



                    //var handler1 = handler;
                    //ThreadPool.QueueUserWorkItem(x => handler1(@event));
                }
            }

            private T CreateInstance<T>(Type type)
            {
                //ToDo - Injection
                return (T)Activator.CreateInstance(type);
            }

            ////http://rogeralsing.com/2008/02/28/linq-expressions-creating-objects/
            //delegate T ObjectActivator<T>(params object[] args);

            //static ObjectActivator<T> GetActivator<T>(ConstructorInfo ctor)
            //{
            //    Type type = ctor.DeclaringType;
            //    ParameterInfo[] paramsInfo = ctor.GetParameters();

            //    //create a single param of type object[]
            //    ParameterExpression param =
            //        Expression.Parameter(typeof(object[]), "args");

            //    Expression[] argsExp =
            //        new Expression[paramsInfo.Length];

            //    //pick each arg from the params array 
            //    //and create a typed expression of them
            //    for (int i = 0; i < paramsInfo.Length; i++)
            //    {
            //        Expression index = Expression.Constant(i);
            //        Type paramType = paramsInfo[i].ParameterType;

            //        Expression paramAccessorExp =
            //            Expression.ArrayIndex(param, index);

            //        Expression paramCastExp =
            //            Expression.Convert(paramAccessorExp, paramType);

            //        argsExp[i] = paramCastExp;
            //    }

            //    //make a NewExpression that calls the
            //    //ctor with the args we just created
            //    NewExpression newExp = Expression.New(ctor, argsExp);

            //    //create a lambda with the New
            //    //Expression as body and our param object[] as arg
            //    LambdaExpression lambda =
            //        Expression.Lambda(typeof(ObjectActivator<T>), newExp, param);

            //    //compile it
            //    ObjectActivator<T> compiled = (ObjectActivator<T>)lambda.Compile();
            //    return compiled;
            //}
        }

}
