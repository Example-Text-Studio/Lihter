using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityRandom = UnityEngine.Random;

namespace Redcode.Extensions
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Get random element from the <paramref name="enumerable"/>.
        /// </summary>
        /// <typeparam name="T">Source type.</typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <returns>Random element from enumerable.</returns>
        public static T GetRandomElement<T>(this IEnumerable<T> enumerable) => enumerable.ElementAt(UnityRandom.Range(0, enumerable.Count()));

        /// <summary>
        /// Get random elements from the <paramref name="enumerable"/>.
        /// </summary>
        /// <typeparam name="T">Source type.</typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="count">Count of the random elements.</param>
        /// <returns>Random elements from enumerable.</returns>
        public static List<T> GetRandomElements<T>(this IEnumerable<T> enumerable, int count)
        {
            var poppedIndexes = Enumerable.Range(0, enumerable.Count()).ToList().PopRandoms(count).Select(p => p.index);
            return enumerable.Where((el, i) => poppedIndexes.Contains(i)).ToList();
        }

        /// <summary>
        /// Excepts passed elements from <paramref name="enumerable"/>.
        /// </summary>
        /// <typeparam name="T">Source type.</typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="elements">Elements to exclude.</param>
        /// <returns>Enumerable without passed elements.</returns>
        public static IEnumerable<T> Except<T>(this IEnumerable<T> enumerable, params T[] elements) => enumerable.Except((IEnumerable<T>)elements);

        /// <summary>
        /// Shuffles <paramref name="enumerable"/>.
        /// </summary>
        /// <typeparam name="T">Source type.</typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <returns>Shuffled <paramref name="enumerable"/>.</returns>
        public static IEnumerable<T> Shuffled<T>(this IEnumerable<T> enumerable) => enumerable.OrderBy(v => UnityRandom.value);

        /// <summary>
        /// Represents an enumerable as a string in the format <see langword="[a, b, c, ...]"/> 
        /// </summary>
        /// <typeparam name="T">Source type.</typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <returns>String representation of the <paramref name="enumerable"/></returns>
        public static string AsString<T>(this IEnumerable<T> enumerable) => $"[{string.Join(", ", enumerable)}]";

        /// <summary>
        /// Get random element index with probability selector.
        /// </summary>
        /// <typeparam name="T">Enumerable elements type.</typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="probabilities">Probabilities, must match in count with enumerable.</param>
        /// <returns>Tuple with random element and it's index.</returns>
        /// <exception cref="ArgumentException">Throwed when <paramref name="enumerable"/> and <paramref name="probabilities"/> counts are not match.</exception>
        public static (T element, int index) GetRandomElementWithProbability<T>(this IEnumerable<T> enumerable, params float[] probabilities) => GetRandomElementWithProbability(enumerable, (IEnumerable<float>)probabilities);

        /// <summary>
        /// <inheritdoc cref="GetRandomElementWithProbability{T}(IEnumerable{T}, float[])"/>
        /// </summary>
        /// <typeparam name="T"><inheritdoc cref="GetRandomElementWithProbability{T}(IEnumerable{T}, float[])"/></typeparam>
        /// <param name="enumerable"><inheritdoc cref="GetRandomElementWithProbability{T}(IEnumerable{T}, float[])" path="/param[@name='enumerable']"/></param>
        /// <param name="probabilities"><inheritdoc cref="GetRandomElementWithProbability{T}(IEnumerable{T}, float[])" path="/param[@name='probabilities']"/></param>
        /// <returns>Tuple with random element and it's index.</returns>
        /// <exception cref="ArgumentException">Throwed when <paramref name="enumerable"/> and <paramref name="probabilities"/> counts are not match.</exception>
        public static (T element, int index) GetRandomElementWithProbability<T>(this IEnumerable<T> enumerable, IEnumerable<float> probabilities)
        {
            var count = enumerable.Count();

            if (probabilities.Count() != count)
                throw new ArgumentException($"Count of probabilities and enumerble elements must be equal.");

            if (count == 0)
                throw new ArgumentException($"Enumerable count must be greater than zero");

            var randomValue = UnityRandom.value * probabilities.Sum();
            var sum = 0f;

            var index = -1;
            var enumerator = probabilities.GetEnumerator();

            while (enumerator.MoveNext())
            {
                index += 1;
                var probability = enumerator.Current;

                sum += probability;

                if (randomValue < sum || randomValue.Approximately(sum))
                    return (enumerable.ElementAt(index), index);
            }

            index = probabilities.Count() - 1;
            return (enumerable.ElementAt(index), index);
        }

        /// <summary>
        /// <inheritdoc cref="GetRandomElementWithProbability{T}(IEnumerable{T}, float[])"/>
        /// </summary>
        /// <typeparam name="T"><inheritdoc cref="GetRandomElementWithProbability{T}(IEnumerable{T}, float[])"/></typeparam>
        /// <param name="enumerable"><inheritdoc cref="GetRandomElementWithProbability{T}(IEnumerable{T}, float[])" path="/param[@name='enumerable']"/></param>
        /// <param name="probabilitySelector">Probabilities selector.</param>
        /// <returns>Tuple with random element and it's index.</returns>
        /// <exception cref="ArgumentException">Throwed when <paramref name="enumerable"/> and <paramref name="probabilities"/> counts are not match.</exception>
        public static (T element, int index) GetRandomElementWithProbability<T>(this IEnumerable<T> enumerable, Func<T, float> probabilitySelector)
        {
            return GetRandomElementWithProbability(enumerable, enumerable.Select(el => probabilitySelector(el)));
        }

        /// <summary>
        /// Loops over all elements.
        /// </summary>
        /// <typeparam name="T">Elements type.</typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="action">What to do with each element?</param>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var element in enumerable)
                action(element);
        }
    }
}