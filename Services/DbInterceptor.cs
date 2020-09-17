using Castle.DynamicProxy;
using System;

 public class DbServiceInterceptor:IInterceptor  
    {  
        public virtual void Intercept(IInvocation invocation)  
        {  
            Console.WriteLine($"{DateTime.Now}: Before method execting. ");  
            invocation.Proceed();  
            Console.WriteLine($"{DateTime.Now}: After method exected.");  
        }  
    } 