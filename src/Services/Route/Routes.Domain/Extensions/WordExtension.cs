using NPOI.XWPF.UserModel;

namespace Routes.Domain.Extensions;

public static class WordExtension
{
    public static void AppendTextLine(this XWPFRun xWpfRun, string text)
    {
        xWpfRun.AppendText(text);
        xWpfRun.AddBreak(BreakType.TEXTWRAPPING);
    }
}