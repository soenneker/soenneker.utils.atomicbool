using System.Runtime.CompilerServices;
using System.Threading;
using Soenneker.Utils.AtomicBool.Abstract;

namespace Soenneker.Utils.AtomicBool;

/// <summary> A thread-safe boolean implemented on top of Interlocked/Volatile operations. </summary>
public sealed class AtomicBool : IAtomicBool
{
    // 0 = false, 1 = true
    private int _flag;

    public AtomicBool(bool initialValue = false) => _flag = initialValue ? 1 : 0;

    public bool Value
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Volatile.Read(ref _flag) != 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => Volatile.Write(ref _flag, value ? 1 : 0);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool CompareAndSet(bool expected, bool newValue)
    {
        int e = expected ? 1 : 0;
        int n = newValue ? 1 : 0;
        return Interlocked.CompareExchange(ref _flag, n, e) == e;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TrySetTrue() => Interlocked.Exchange(ref _flag, 1) == 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TrySetFalse() => Interlocked.Exchange(ref _flag, 0) == 1;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Exchange(bool newValue) => Interlocked.Exchange(ref _flag, newValue ? 1 : 0) == 1;

    public bool IsTrue => Volatile.Read(ref _flag) != 0;

    public bool IsFalse => Volatile.Read(ref _flag) == 0;

    public override string ToString() => Value.ToString();
}