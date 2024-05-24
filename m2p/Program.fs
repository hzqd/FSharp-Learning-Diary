open System.IO
open QuestPDF.Fluent
open QuestPDF.Markdown
open QuestPDF.Infrastructure
open QuestPDF.Helpers

QuestPDF.Settings.License <- LicenseType.Community
   
let read = File.ReadAllText

let options = MarkdownRendererOptions(
    BlockQuoteBorderThickness = 1,
    BlockQuoteBorderColor = Colors.Brown.Medium,
    BlockQuoteTextColor = Colors.Grey.Darken3,
    CodeBlockBackground = "#CDFFCC"
)

let document (s:string) = Document.Create(fun container ->
    container
        .Page(fun page ->
            page.Size PageSizes.A4
            page.PageColor "#FEFFDD"
            page.Margin 25.f // 页面边距
            page.DefaultTextStyle(TextStyle.Default.FontSize 13.f) // 设置默认字体大小
            page.Content().Column((fun column ->
                column.Spacing 10.f)) // 段落间距
            page.Content().Markdown(s, options)
        )
    |> ignore)

let gen (sout:string) sin = document(sin).GeneratePdf(sout)

[<EntryPoint>]
let main args = 
    let name = args[0]
    (name, ".pdf") |> Path.ChangeExtension |> (name |> read |> gen)
    0