using System.Text;

namespace MEGA_CLI.utils
{
    internal class RedirectWriter : TextWriter
    {
        private TextWriter PrimaryWriter { get; init; } = StreamWriter.Null;
        private TextWriter SecondaryWriter { get; init; } = StreamWriter.Null;

        public override Encoding Encoding => PrimaryWriter.Encoding;

        public override void Write(ReadOnlySpan<char> buffer)
        {
            PrimaryWriter.Write(buffer);
            SecondaryWriter.Write(buffer);
        }
    }
}