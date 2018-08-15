using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Store;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Documents;
using System.Xml.Linq;

namespace LuceneSample1
{

    [TestClass]
    public class UnitTest1
    {
        const string indexFolder = @"..\..\..\Index";
        const string baseFolder = @"..\..\..\App_Data";

        [TestMethod]
        public void BuildIndex()
        {
            var dirInfo = new System.IO.DirectoryInfo(indexFolder);

            using (var analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30))
            using (var indexDir = FSDirectory.Open(dirInfo))
            {
                using (var indexWriter = new IndexWriter(indexDir, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
                {
                    foreach (var fileName in System.IO.Directory.GetFiles(baseFolder))
                    {
                        var xmlDoc = XDocument.Load(fileName);

                        string docid = xmlDoc.Root.Attribute("docid").Value;

                        // remove older index entry
                        var searchQuery = new TermQuery(new Term("exact", docid));
                        indexWriter.DeleteDocuments(searchQuery);

                        // Create a Lucene document...
                        Document document = new Document();

                        document.Add(new Field("exact", docid, Field.Store.YES, Field.Index.NOT_ANALYZED));

                        foreach (var element in xmlDoc.Root.Descendants())
                        {
                            var name = element.Name.LocalName.ToLower();
                            var value = (element.Value ?? string.Empty).Trim().ToUpper();

                            switch (name)
                            {
                                case "rank":
                                    var numericField = new NumericField("rank", Field.Store.YES, true);
                                    numericField.SetIntValue(Convert.ToInt32(value));
                                    document.Add(numericField);
                                    break;
                                case "doj":
                                    var dateField = new Field(name,
                                    DateTools.DateToString(Convert.ToDateTime(value), DateTools.Resolution.MILLISECOND),
                                    Field.Store.YES, Field.Index.NOT_ANALYZED);
                                    document.Add(dateField);
                                    break;
                                default:
                                    var field = new Field(name, value, Field.Store.YES, Field.Index.ANALYZED);
                                    document.Add(field);
                                    break;
                            }
                        }

                        //add doc
                        indexWriter.AddDocument(document);
                    }
                }
            }
        }
    }
}