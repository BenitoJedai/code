using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.Func<>))]
    internal delegate T __Func<T>();

    [Script(Implements = typeof(global::System.Func<>))]
    internal delegate TResult __Func<A, TResult>(A a);

    [Script(Implements = typeof(global::System.Func<,>))]
    internal delegate TResult __Func<A, B, TResult>(A a, B b);

    [Script(Implements = typeof(global::System.Func<,,>))]
    internal delegate TResult __Func<A, B, C, TResult>(A a, B b, C c);

    [Script(Implements = typeof(global::System.Func<,,,>))]
    internal delegate TResult __Func<A, B, C, D, TResult>(A a, B b, C c, D d);

    [Script(Implements = typeof(global::System.Func<,,,,>))]
    internal delegate TResult __Func<A, B, C, D, E, TResult>(A a, B b, C c, D d, E e);

    [Script(Implements = typeof(global::System.Func<,,,,,>))]
    internal delegate TResult __Func<A, B, C, D, E, F, TResult>(A a, B b, C c, D d, E e, F f);

    [Script(Implements = typeof(global::System.Func<,,,,,,>))]
    internal delegate TResult __Func<A, B, C, D, E, F, G, TResult>(A a, B b, C c, D d, E e, F f, G g);

    [Script(Implements = typeof(global::System.Func<,,,,,,,>))]
    internal delegate TResult __Func<A, B, C, D, E, F, G, H, TResult>(A a, B b, C c, D d, E e, F f, G g, H h);

    [Script(Implements = typeof(global::System.Func<,,,,,,,,>))]
    internal delegate TResult __Func<A, B, C, D, E, F, G, H, I, TResult>(A a, B b, C c, D d, E e, F f, G g, H h, I i);

    [Script(Implements = typeof(global::System.Func<,,,,,,,,,>))]
    internal delegate TResult __Func<A, B, C, D, E, F, G, H, I, J, TResult>(A a, B b, C c, D d, E e, F f, G g, H h, I i, J j);

    [Script(Implements = typeof(global::System.Func<,,,,,,,,,,>))]
    internal delegate TResult __Func<A, B, C, D, E, F, G, H, I, J, K, TResult>(A a, B b, C c, D d, E e, F f, G g, H h, I i, J j, K k);

    [Script(Implements = typeof(global::System.Func<,,,,,,,,,,,>))]
    internal delegate TResult __Func<A, B, C, D, E, F, G, H, I, J, K, L, TResult>(A a, B b, C c, D d, E e, F f, G g, H h, I i, J j, K k, L l);

    [Script(Implements = typeof(global::System.Func<,,,,,,,,,,,,>))]
    internal delegate TResult __Func<A, B, C, D, E, F, G, H, I, J, K, L, M, TResult>(A a, B b, C c, D d, E e, F f, G g, H h, I i, J j, K k, L l, M m);

    [Script(Implements = typeof(global::System.Func<,,,,,,,,,,,,,>))]
    internal delegate TResult __Func<A, B, C, D, E, F, G, H, I, J, K, L, M, N, TResult>(A a, B b, C c, D d, E e, F f, G g, H h, I i, J j, K k, L l, M m, N n);

    [Script(Implements = typeof(global::System.Func<,,,,,,,,,,,,,,>))]
    internal delegate TResult __Func<A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, TResult>(A a, B b, C c, D d, E e, F f, G g, H h, I i, J j, K k, L l, M m, N n, O o);


}
