using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;

[Intercept (typeof (DbServiceInterceptor))]
public class DbService : IDbService {

    public string Say () {
        return "Hello";
    }
}