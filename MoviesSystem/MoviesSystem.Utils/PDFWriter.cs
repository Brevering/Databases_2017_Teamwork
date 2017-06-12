using iTextSharp.text;
using iTextSharp.text.pdf;
using MoviesSystem.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System;

namespace MoviesSystem.Utils
{
    public class PDFWriter
    {
        public void Write(ObservableCollection<Movie> moviesCollection)
        {
            FileStream fs = new FileStream("../../../../Data/exportedData.pdf", FileMode.Create, FileAccess.Write, FileShare.None);

            Document document = new Document(this.CreatePdfRectangle());

            PdfWriter writer = PdfWriter.GetInstance(document, fs);

            this.FillDocument(document, moviesCollection);
        }

        private Rectangle CreatePdfRectangle()
        {
            Rectangle rect = new Rectangle(PageSize.A3);
            rect.BackgroundColor = new BaseColor(100, 100, 100);
            return rect;
        }

        private void FillDocument(Document document, ObservableCollection<Movie> moviesCollection)
        {
            document.Open();

            this.GenerateData(document, moviesCollection);

            document.Close();
        }

        private void GenerateData(Document document, ObservableCollection<Movie> moviesCollection)
        {
            var movies = moviesCollection.OrderBy(m => m.Title)
                                .Select(m => new Movie()
                                {
                                    Title = m.Title,
                                    Genres = m.Genres.OrderBy(g => g.Name).ToList(),
                                    Description = m.Description,
                                    Rate = m.Rate,
                                   // Actors = m.Actors.OrderBy(a => a.FirstName).ToList()
                                }).ToList();

            PdfPTable table = new PdfPTable(4);

            int[] widths = new int[] { 20, 20, 40, 10 };

            table.SetWidths(widths);

            table.AddCell(this.CreateCell(new Phrase("Movie title"), true));
            table.AddCell(this.CreateCell(new Phrase("Genres"), true));
            table.AddCell(this.CreateCell(new Phrase("Description"), true));
            table.AddCell(this.CreateCell(new Phrase("Rate"), true));
    //        table.AddCell(this.CreateCell(new Phrase("Actors"), true));

            this.InputData(movies, table);

            document.Add(table);
        }

        private void InputData(List<Movie> movies, PdfPTable table)
        {
            for (int i = 0; i < movies.Count; i++)
            {
                table.AddCell(this.CreateCell(new Phrase(movies[i].Title)));
                table.AddCell(this.CreateCell(new Phrase(ExtractGenres(movies[i].Genres))));
                table.AddCell(this.CreateCell(new Phrase(movies[i].Description.Summary)));
                table.AddCell(this.CreateCell(new Phrase(ExtractRate(movies[i].Rate))));
            }
        }

        private string ExtractRate(Rate rate)
        {
            var result = rate.RateValue.ToString();

            return result;
        }

        private string ExtractGenres(ICollection<Genre> genres)
        {
            var result = new StringBuilder();

            foreach (var genre in genres)
            {
                result.Append(genre.Name);
            }

            return result.ToString();
        }

        private PdfPCell CreateCell(Phrase phrase, bool isHeader = false, int cellColspan = 0)
        {
            PdfPCell cell = new PdfPCell(phrase);
            if (isHeader)
            {
                cell.BackgroundColor = new BaseColor(102, 153, 153);
            }
            else
            {
                cell.BackgroundColor = new BaseColor(204, 204, 255);
            }

            cell.Colspan = cellColspan;
            cell.MinimumHeight = 25;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.HorizontalAlignment = 1;

            return cell;
        }
    }
}
