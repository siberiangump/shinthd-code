public interface Injection<Type>
{
    void Inject(Type data);
}

public interface Injection<Type, Marker>
{
    void Inject(Type data);
}

public interface INestedInjectionRequired
{
    object[] InjectionLiteners();
}

public interface IInjector
{
    object[] InternalMembers();
    void Inject(object target);
}

public static class Injector
{
    public static void Inject<Type>(this object target, Type data)
    {
        (target as Injection<Type>)?.Inject(data);
        if (target is INestedInjectionRequired nestedInjection)
            foreach (var item in nestedInjection.InjectionLiteners())
                item.Inject(data);
    }

    public static void Inject<Type, Marker>(this object target, Type data)
    {
        (target as Injection<Type, Marker>)?.Inject(data);
        if (target is INestedInjectionRequired nestedInjection)
            foreach (var item in nestedInjection.InjectionLiteners())
                item.Inject<Type, Marker>(data);
    }

    public static void InternalResolve(this IInjector target)
    {
        foreach (var item in target.InternalMembers())
        {
            target.Inject(item);
        }
    }

    public static void ExternalResolve(this IInjector target, params IInjector[] injectors)
    {
        foreach (var injector in injectors)
        {
            foreach (var item in target.InternalMembers())
                injector.Inject(item);
        }
    }

    public static void ResolveInjectors(params IInjector[] injectors)
    {
        foreach (var targetInjector in injectors)
        {
            foreach (var injector in injectors)
            {
                if (targetInjector == injector)
                    continue;
                foreach (var item in targetInjector.InternalMembers())
                    injector.Inject(item);
            }
        }
    }
}