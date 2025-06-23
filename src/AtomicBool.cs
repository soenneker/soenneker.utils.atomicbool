using System.Threading;

namespace Soenneker.Utils.AtomicBool;

/// <summary>
/// A thread-safe boolean implemented on top of Interlocked operations.
/// </summary>
public sealed class AtomicBool
{
    // Underlying 0/1 storage
    private int _flag;

    /// <summary>
    /// Initializes a new AtomicBool with the given initial value.
    /// </summary>
    public AtomicBool(bool initialValue = false)
    {
        _flag = initialValue ? 1 : 0;
    }

    /// <summary>
    /// Gets or sets the current value atomically.
    /// </summary>
    public bool Value
    {
        get => Interlocked.CompareExchange(ref _flag, 0, 0) == 1;
        set => Interlocked.Exchange(ref _flag, value ? 1 : 0);
    }

    /// <summary>
    /// Atomically sets the value to <paramref name="newValue"/> if the current value equals <paramref name="expected"/>.
    /// Returns true if successful.
    /// </summary>
    public bool CompareAndSet(bool expected, bool newValue)
    {
        int e = expected ? 1 : 0;
        int n = newValue ? 1 : 0;

        return Interlocked.CompareExchange(ref _flag, n, e) == e;
    }

    /// <summary>
    /// Atomically sets the flag to true and returns whether it was false before.
    /// </summary>
    public bool TrySetTrue() => CompareAndSet(false, true);

    /// <summary>
    /// Atomically sets the flag to false and returns whether it was true before.
    /// </summary>
    public bool TrySetFalse() => CompareAndSet(true, false);

    /// <summary>
    /// Atomically exchanges the flag and returns its previous value.
    /// </summary>
    public bool Exchange(bool newValue)
    {
        int n = newValue ? 1 : 0;
        return Interlocked.Exchange(ref _flag, n) == 1;
    }

    public override string ToString() => Value.ToString();
}
