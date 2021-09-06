using System;
using System.Text;

namespace Tclg.Text
{
    public static class UTF8EncodingExtensions
    {
        public static bool HasBOM(this UTF8Encoding @this) => !@this.Preamble.IsEmpty;
    }
}
