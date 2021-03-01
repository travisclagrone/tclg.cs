using System;
using System.Collections.Generic;
using System.Linq;

namespace Tclg.Text
{
    public static class StringExtensions
    {
        #region Accessors

        #region Predicate Searches

        public static bool ContainsAll(this string @this, IList<string> values) => values.All(@value => @this.Contains(@value));
        public static bool ContainsAny(this string @this, IList<string> values) => values.Any(@value => @this.Contains(@value));

        public static bool StartsWithAny(this string @this, IList<string> values) => values.Any(@value => @this.StartsWith(@value));
        public static bool EndsWithAny(this string @this, IList<string> values) => values.Any(@value => @this.EndsWith(@value));

        public static bool SurroundedBy(this string @this, string @value)                      => @this.StartsWith(@value)     && @this.EndsWith(@value);
        public static bool SurroundedBy(this string @this, string startValue, string endValue) => @this.StartsWith(startValue) && @this.EndsWith(endValue);
        public static bool SurroundedByAny(this string @this, IList<string> values) => values.Any(@value => @this.StartsWith(@value) && @this.EndsWith(@value));

        #endregion

        #region Semantic Conversions

        public static string DefaultIfEmpty(this string @this, string @default)             => String.IsNullOrEmpty(@this)      ? @default : @this;
        public static string DefaultIfEmptyOrWhitespace(this string @this, string @default) => String.IsNullOrWhiteSpace(@this) ? @default : @this;

        public static string? NullIfEmpty(this string @this)             => String.IsNullOrEmpty(@this)      ? null : @this;
        public static string? NullIfEmptyOrWhitespace(this string @this) => String.IsNullOrWhiteSpace(@this) ? null : @this;

        #endregion

        #region IndexOf Extensions

        public enum DefaultIndex
        {
            Zero,
            StartIndex,
            EndIndex,
            Length,
        }

        private static int ResolveDefaultIndex(string @string, DefaultIndex @default) => @default switch
        {
            DefaultIndex.Zero => 0,
            DefaultIndex.StartIndex => 0,
            DefaultIndex.EndIndex => @string.Length,
            DefaultIndex.Length => @string.Length,
            _ => throw new NotImplementedException(),
        };

        private static int ResolveDefaultIndex(string @string, int startIndex, DefaultIndex @default) => @default switch
        {
            DefaultIndex.Zero => 0,
            DefaultIndex.StartIndex => startIndex,
            DefaultIndex.EndIndex => @string.Length,
            DefaultIndex.Length => @string.Length,
            _ => throw new NotImplementedException(),
        };

        private static int ResolveDefaultIndex(string @string, int startIndex, int count, DefaultIndex @default) => @default switch
        {
            DefaultIndex.Zero => 0,
            DefaultIndex.StartIndex => startIndex,
            DefaultIndex.EndIndex => startIndex + count,
            DefaultIndex.Length => @string.Length,
            _ => throw new NotImplementedException(),
        };

        #region IndexOrDefault

        public static int IndexOrDefault(this string @this, string @value, DefaultIndex @default)
        {
            int index = @this.IndexOf(@value);
            return index != -1 ? index : ResolveDefaultIndex(@this, @value, @default);
        }

        public static int IndexOrDefault(this string @this, string @value, DefaultIndex @default, StringComparison comparisonType)
        {
            int index = @this.IndexOf(@value, comparisonType);
            return index != -1 ? index : ResolveDefaultIndex(@this, @value, @default);
        }

        public static int IndexOrDefault(this string @this, string @value, int startIndex, DefaultIndex @default)
        {
            int index = @this.IndexOf(@value, startIndex);
            return index != -1 ? index : ResolveDefaultIndex(@this, @value, startIndex, @default);
        }

        public static int IndexOrDefault(this string @this, string @value, int startIndex, DefaultIndex @default, StringComparison comparisonType)
        {
            int index = @this.IndexOf(@value, startIndex, comparisonType);
            return index != -1 ? index : ResolveDefaultIndex(@this, @value, startIndex, @default);
        }

        public static int IndexOrDefault(this string @this, string @value, int startIndex, int count, DefaultIndex @default)
        {
            int index = @this.IndexOf(@value, startIndex, count);
            return index != -1 ? index : ResolveDefaultIndex(@this, @value, startIndex, count, @default);
        }

        public static int IndexOrDefault(this string @this, string @value, int startIndex, int count, DefaultIndex @default, StringComparison comparisonType)
        {
            int index = @this.IndexOf(@value, startIndex, count, comparisonType);
            return index != -1 ? index : ResolveDefaultIndex(@this, @value, startIndex, count, @default);
        }

        #endregion

        #region LastIndexOrDefault

        public static int LastIndexOrDefault(this string @this, string @value, DefaultIndex @default)
        {
            int index = @this.LastIndexOf(@value);
            return index != -1 ? index : ResolveDefaultIndex(@this, @value, @default);
        }

        public static int LastIndexOrDefault(this string @this, string @value, DefaultIndex @default, StringComparison comparisonType)
        {
            int index = @this.LastIndexOf(@value, comparisonType);
            return index != -1 ? index : ResolveDefaultIndex(@this, @value, @default);
        }

        public static int LastIndexOrDefault(this string @this, string @value, int startIndex, DefaultIndex @default)
        {
            int index = @this.LastIndexOf(@value, startIndex);
            return index != -1 ? index : ResolveDefaultIndex(@this, @value, startIndex, @default);
        }

        public static int LastIndexOrDefault(this string @this, string @value, int startIndex, DefaultIndex @default, StringComparison comparisonType)
        {
            int index = @this.LastIndexOf(@value, startIndex, comparisonType);
            return index != -1 ? index : ResolveDefaultIndex(@this, @value, startIndex, @default);
        }

        public static int LastIndexOrDefault(this string @this, string @value, int startIndex, int count, DefaultIndex @default)
        {
            int index = @this.LastIndexOf(@value, startIndex, count);
            return index != -1 ? index : ResolveDefaultIndex(@this, @value, startIndex, count, @default);
        }

        public static int LastIndexOrDefault(this string @this, string @value, int startIndex, int count, DefaultIndex @default, StringComparison comparisonType)
        {
            int index = @this.LastIndexOf(@value, startIndex, count, comparisonType);
            return index != -1 ? index : ResolveDefaultIndex(@this, @value, startIndex, count, @default);
        }

        #endregion

        #endregion

        #region Substring Extensions

        public enum DefaultSubstring
        {
            EmptyString,
            FullString,
        }

        private static string ResolveDefaultSubstring(string @string, DefaultSubstring @default) => @default switch
        {
            DefaultSubstring.EmptyString => String.Empty,
            DefaultSubstring.FullString => @string,
            _ => throw new NotImplementedException(),
        };

        private const string ValueNotFoundErrorMessage = "value was not found in this string.";

        #region SubstringStart

        // This method will throw if there is more length requested than is available.
        public static string SubstringStart(this string @this, int length) => @this.Substring(0, length);

        // This method will return the entire input string (i.e. `@this`) without error if more length is requested than is available.
        // This method will return null if the requested length is negative.
        public static string? TrySubstringStart(this string @this, int length)
        {
            if (length < 0)
            {
                return null;
            }
            else if (length <= @this.Length)
            {
                return @this.Substring(0, length);
            }
            else
            {
                return @this;
            }
        }

        #endregion

        #region SubstringEnd

        // This method will throw if there is more length requested than is available.
        public static string SubstringEnd(this string @this, int length) => @this.Substring(@this.Length - length, length);

        // This method will return the entire input string (i.e. `@this`) without error if more length is requested than is available.
        // This method will return null if the requested length is negative.
        public static string? TrySubstringEnd(this string @this, int length)
        {
            if (length < 0)
            {
                return null;
            }
            else if (length <= @this.Length)
            {
                return @this.Substring(@this.Length - length, length);
            }
            else
            {
                return @this;
            }
        }

        #endregion

        #region SubstringBefore

        public static string SubstringBeforeFirst(this string @this, string @value, bool includeValue = false)
        {
            int index = @this.IndexOf(@value);
            if (index != -1)
            {
                return @this.Substring(0, index + (includeValue ? @value.Length : 0));
            }
            else
            {
                throw new ArgumentOutOfRangeException(ValueNotFoundErrorMessage);
            }
        }

        public static string SubstringBeforeFirstOrDefault(this string @this, string @value, bool includeValue = false, DefaultSubstring @default = DefaultSubstring.EmptyString)
        {
            int index = @this.IndexOf(@value);
            if (index != -1)
            {
                return @this.Substring(index + (includeValue ? 0 : @value.Length));
            }
            else
            {
                return ResolveDefaultSubstring(@this, @default);
            }
        }

        public static string? TrySubstringBeforeFirst(this string @this, string @value, bool includeValue = false)
        {
            int index = @this.IndexOf(@value);
            if (index != -1)
            {
                return @this.Substring(index + (includeValue ? @value.Length : 0));
            }
            else
            {
                return null;
            }
        }

        // TODO SubstringBeforeLast
        // TODO SubstringBeforeLastOrDefault
        // TODO TrySubstringBeforeLast

        #endregion

        #region SubstringAfter

        public static string SubstringAfterFirst(this string @this, string @value, bool includeValue = false)
        {
            int index = @this.IndexOf(@value);
            if (index != -1)
            {
                return @this.Substring(index + (includeValue ? 0 : @value.Length));
            }
            else
            {
                throw new ArgumentOutOfRangeException(ValueNotFoundErrorMessage);
            }
        }

        public static string SubstringAfterFirstOrDefault(this string @this, string @value, bool includeValue = false, DefaultSubstring @default = DefaultSubstring.EmptyString)
        {
            int index = @this.IndexOf(@value);
            if (index != -1)
            {
                return @this.Substring(index + (includeValue ? 0 : @value.Length));
            }
            else
            {
                return ResolveDefaultSubstring(@this, @default);
            }
        }

        public static string? TrySubstringAfterFirst(this string @this, string @value, bool includeValue = false)
        {
            int index = @this.IndexOf(@value);
            if (index != -1)
            {
                return @this.Substring(index + (includeValue ? 0 : @value.Length));
            }
            else
            {
                return null;
            }
        }

        // TODO SubstringAfterLast
        // TODO SubstringAfterLastOrDefault
        // TODO TrySubstringAfterLast

        #endregion

        #endregion

        #endregion

        #region Mutators

        // TODO Insert extensions

        // TODO Replace extensions

        // TODO Remove extensions

        #endregion Mutators
    }
}
