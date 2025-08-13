using System.Threading;

namespace Soenneker.Utils.AtomicBool.Abstract;

/// <summary>
/// A lock-free, atomic boolean implemented over an <see cref="int"/> using <see cref="Interlocked"/>,
/// </summary>
public interface IAtomicBool
{
    /// <summary>
    /// Gets or sets the current value with appropriate memory ordering.
    /// </summary>
    /// <remarks>
    /// Reading should use acquire semantics; writing should use release semantics.
    /// </remarks>
    bool Value { get; set; }

    /// <summary>
    /// Atomically sets the value to <paramref name="newValue"/> if the current value equals <paramref name="expected"/>.
    /// </summary>
    /// <param name="expected">The value to compare against the current value.</param>
    /// <param name="newValue">The value to set if the comparison succeeds.</param>
    /// <returns><see langword="true"/> if the value was updated; otherwise, <see langword="false"/>.</returns>
    bool CompareAndSet(bool expected, bool newValue);

    /// <summary>
    /// Atomically sets the flag to <see langword="true"/> and returns whether it was <see langword="false"/> before.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if the previous value was <see langword="false"/>; otherwise, <see langword="false"/>.
    /// </returns>
    bool TrySetTrue();

    /// <summary>
    /// Atomically sets the flag to <see langword="false"/> and returns whether it was <see langword="true"/> before.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if the previous value was <see langword="true"/>; otherwise, <see langword="false"/>.
    /// </returns>
    bool TrySetFalse();

    /// <summary>
    /// Atomically replaces the current value with <paramref name="newValue"/> and returns the previous value.
    /// </summary>
    /// <param name="newValue">The value to set.</param>
    /// <returns>The previous value before the exchange.</returns>
    bool Exchange(bool newValue);

    /// <summary>
    /// Gets a value indicating whether the current value is <see langword="true"/>.
    /// </summary>
    bool IsTrue { get; }

    /// <summary>
    /// Gets a value indicating whether the current value is <see langword="false"/>.
    /// </summary>
    bool IsFalse { get; }
}