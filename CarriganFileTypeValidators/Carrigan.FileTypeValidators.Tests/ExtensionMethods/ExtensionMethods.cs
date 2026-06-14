using System;
using System.Collections.Generic;
using System.Text;

namespace Carrigan.FileTypeValidators.Tests.ExtensionMethods;

internal static class ExtensionMethods
{
    /// <summary>
    /// Returns a new enumerable where the value at the specified index is replaced with the provided value, and all other values remain the same.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the values in the enumerable.
    /// </typeparam>
    /// <param name="source">
    /// The source enumerable. Cannot be null.
    /// </param>
    /// <param name="index">
    /// The index of the value to replace.
    /// </param>
    /// <param name="value">
    /// The value to replace with.
    /// </param>
    /// <returns>
    /// A new enumerable where the value at the specified index is replaced with the provided value.
    /// </returns>
    internal static IEnumerable<T> ReplaceAt<T>(this IEnumerable<T> source, int index, T value)
    {
        ArgumentNullException.ThrowIfNull(source);

        ArgumentOutOfRangeException.ThrowIfNegative(index, nameof(index));
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index,source.Count(), nameof(index));
        int currentIndex = 0;

        foreach (T currentValue in source)
        {
            if (currentIndex == index)
            {
                yield return value;
            }
            else
            {
                yield return currentValue;
            }

            currentIndex++;
        }
    }

    /// <summary>
    /// Creates a cycler function that returns the next value in the provided enumerable each time it is called, and loops back to the beginning after reaching the end. 
    /// The returned function is not thread safe.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the values in the enumerable.
    /// </typeparam>
    /// <param name="values">
    /// The enumerable of values to cycle through. Must contain at least one item.
    /// </param>
    /// <returns>
    /// A function that returns the next value in the enumerable each time it is called, looping back to the beginning after reaching the end.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the provided enumerable is empty.
    /// </exception>
    internal static Func<T> CreateCycler<T>(this IEnumerable<T> values)
    {
        ArgumentNullException.ThrowIfNull(values);

        T[] items = [.. values];

        if (items.Length == 0)
        {
            throw new ArgumentException("The enumerable must contain at least one item.", nameof(values));
        }

        int index = -1;

        return () =>
        {
            index = (index + 1) % items.Length;
            return items[index];
        };
    }


    /// <summary>
    /// Returns a new byte array where each null value in the input array is replaced with the next value from the provided fillFrom array, cycling through fillFrom if necessary.
    /// </summary>
    /// <param name="values">
    /// The input array of nullable bytes. Cannot be null.
    /// </param>
    /// <param name="fillFrom">
    /// The array of bytes to use for filling null values. Cannot be null.
    /// </param>
    /// <returns>
    /// A new byte array where each null value in the input array is replaced with the next value from the provided fillFrom array, cycling through fillFrom if necessary.
    /// </returns>
    internal static byte[] FillNullsWithValues(this byte?[] values, byte[] fillFrom)
    {
        ArgumentNullException.ThrowIfNull(values);
        ArgumentNullException.ThrowIfNull(fillFrom);

        Func<byte> cycler = fillFrom.CreateCycler();

        byte[] result = new byte[values.Length];

        for (int index = 0; index < values.Length; index++)
        {
            result[index] = values[index] ?? cycler();
        }

        return result;
    }
}
