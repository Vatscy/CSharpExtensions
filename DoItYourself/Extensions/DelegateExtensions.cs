
namespace System
{
    public static class DelegateExtensions
    {
        public static Func<T1, Func<T2, TResult>> Currying<T1, T2, TResult>(this Func<T1, T2, TResult> func)
        {
            return t1 => t2 => func(t1, t2);
        }

        public static Func<T1, Func<T2, Func<T3, TResult>>> Currying<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> func)
        {
            return t1 => t2 => t3 => func(t1, t2, t3);
        }
    }
}

