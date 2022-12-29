namespace NET7Tests.CSharp11Features;

public class ListPatterns
{
    [Test]
    public void TestListPatterns()
    {
        var list = new List<int>();
        Assert.IsTrue(list is [], "list is Empty");
        IList<int> enumerable = Enumerable.Range(0, 10).ToList();
        Assert.IsTrue(enumerable is [0, ..], "enumerable has at least 1 item which is 0");
        Assert.IsTrue(enumerable is [.., 9], "enumerable has at least 1 item and ends with 9");
        // Slice patterns may not be used for a value of type 'System.Collections.Generic.List<int>'. No suitable range indexer or 'Slice' method was found:
        // enumerable is [0, 1, ..{Count: > 5}, 9]
        Assert.IsTrue(enumerable.ToArray() is [0, 1, .. {Length: > 5}, 9], "enumerable starts with 0, 1, ends with 9 and has at least 5 items in-between");
    }
    
    internal static (List<MailToSend> mailToSends, List<Report> reportsNotUsed) CreateMailsWithReports(
        IList<Report> reports,
        IEnumerable<ReportReceiver> reportReceivers,
        long maxAttachmentSizeInBytes)
    {
        var mailToSends = reportReceivers.Join(reports,
                outerKeySelector: receiver => new { receiver.ReportTypeId, receiver.FundId },
                innerKeySelector: report => new { report.ReportTypeId, report.FundId },
                resultSelector: (receiver, report) => new
                {
                    Receiver = receiver,
                    Report = report
                })
            .GroupBy(x => x.Receiver.Email, x => x.Report)
            .SelectMany(g => ChunkByMaxSize(g)
                .Select(r => new MailToSend
                {
                    CustomerEmail = g.Key,
                    Reports = r
                }))
            .ToList();
        
        // warn about reports without receivers 
        var reportsNotUsed = reports.Except(mailToSends.SelectMany(x => x.Reports)).ToList();
    
        return (mailToSends, reportsNotUsed);

        IEnumerable<List<Report>> ChunkByMaxSize(IEnumerable<Report> reports)
        {
            var agg = new List<Report>();
            long byteUsed = 0;
            foreach (var rpt in reports)
            {
                if (byteUsed + rpt.SizeInBytes <= maxAttachmentSizeInBytes)
                {
                    byteUsed += rpt.SizeInBytes;
                    agg.Add(rpt);
                }
                else
                {
                    yield return agg;
                    agg = new List<Report> {rpt};
                    byteUsed = rpt.SizeInBytes;
                }
            }

            yield return agg;
        }
    }
}

internal class ReportReceiver
{
    public int ReportTypeId { get; set; }
    public int FundId { get; set; }
    public string Email { get; set; }
}

internal class Report

{
    public int ReportTypeId { get; set; }
    public int FundId { get; set; }
    public long SizeInBytes { get; set; }
}

internal class MailToSend

{
    public string CustomerEmail { get; set; }
    public List<Report> Reports { get; set; }
}