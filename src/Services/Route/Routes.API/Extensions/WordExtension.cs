using NPOI.XWPF.UserModel;

namespace Routes.API.Extensions
{
    public static class WordExtension
    {
        public static void AppendTextLine(this XWPFRun xWPFRun, string text)
        {
            xWPFRun.AppendText(text);
            xWPFRun.AddBreak(BreakType.TEXTWRAPPING);
        }
    }
}
