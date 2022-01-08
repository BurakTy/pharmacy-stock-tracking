using Business.Abstract;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;
using iText.IO.Font;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout.Borders;
using iText.Kernel.Pdf;
using iText.Kernel.Font;
using iText.Kernel.Events;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Geom;
using iText.Layout.Renderer;
using iText.Layout.Layout;
using iText.Kernel.Colors;
using iText.IO.Image;
using iText.Kernel.Pdf.Extgstate;

namespace Business.Concrete
{
    public class PdfManager : IPdfService
    {
        IStokCikisService _stokCikisService;
        IStokCikisDetayService _stokCikisDetayService;
        IStokFaturaService _stokFaturaService;
        public static readonly String IMAGE = @"https://angular.io/assets/images/logos/angular/angular.svg";
        public static readonly String IMAGEH = @"D:\Yedek\hastanebaslik.png";


        public PdfManager(IStokCikisService stokCikisService, IStokCikisDetayService stokCikisDetayService, IStokFaturaService stokFaturaService)
        {
            _stokCikisService = stokCikisService;
            _stokCikisDetayService = stokCikisDetayService;
            _stokFaturaService = stokFaturaService;
        }

        public IDataResult<byte[]> FaturaPdf(int PkFatura)
        {
            var stokFaturaBilgi = _stokFaturaService.GetFaturaBilgi(PkFatura).Data;
            var stokFaturaDetay = _stokFaturaService.GetFaturaDetay(PkFatura).Data;
            try
            {
                using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                {
                    PdfFont font = PdfFontFactory.CreateFont(FontConstants.TIMES_ROMAN);
                    PdfFont bold = PdfFontFactory.CreateFont(FontConstants.TIMES_BOLD);
                    PdfWriter writer = new PdfWriter(memoryStream);
                    PdfDocument pdf = new PdfDocument(writer);
                    PageSize pageSize = PageSize.A4;
                    Document document = new Document(pdf, pageSize);
                    document.SetMargins(150, 35, 70, 35);
                    document.SetFontSize(10);
                    float[] widths = { 1, 5, 2, 2, 2, 2, 2 };
                    pdf.AddEventHandler(PdfDocumentEvent.START_PAGE, new PageBorderEventHandler());

                    PageBackgroundEventHandler pageBacground = new PageBackgroundEventHandler(IMAGE);
                    pdf.AddEventHandler(PdfDocumentEvent.START_PAGE, pageBacground);


                    #region Header
                    Table table = new Table(new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }).UseAllAvailableWidth();
                    table.SetFontSize(10);
                    Cell cell;
                    cell = new Cell(4, 4);//
                    Paragraph p = new Paragraph();
                    p.Add(new Text("Nd-ja "));
                    p.Add(new Text("Spitali Rajonal Memorial Fier").SetUnderline());
                    cell.Add(p);
                    cell.SetBorderBottom(Border.NO_BORDER);
                    cell.SetBorderRight(Border.NO_BORDER);
                    cell.SetPadding(5);
                    table.AddCell(cell);

                    cell = new Cell(4, 6);
                    cell.SetTextAlignment(TextAlignment.CENTER);
                    cell.Add(new Paragraph("FLËTE - HYRJE").SetFont(bold).SetFontSize(20));
                    cell.Add(new Paragraph(""));
                    p = new Paragraph();
                    p.Add(new Text("Nr. "));
                    p.Add(new Text(stokFaturaBilgi.FaturaNo).SetUnderline());
                    p.Add(new Text("  Dt. "));
                    p.Add(new Text(stokFaturaBilgi.FaturaTarih.ToString("dd.MM.yyyy")).SetUnderline());
                    p.SetMarginTop(10);
                    cell.Add(p);
                    cell.SetWidth(250);
                    cell.SetBorderBottom(Border.NO_BORDER);
                    cell.SetBorderLeft(Border.NO_BORDER);
                    table.AddCell(cell);


                    cell = new Cell(6, 2);
                    //cell.Add(new Paragraph("Adresa nga viewn malli"));
                    //cell.Add(new Paragraph("Çepni, İnönü Cd. No:176, 19040 Merkez/Çorum ").SetUnderline());
                    cell.Add(new Paragraph(stokFaturaBilgi.GelenBilgiUnvan).SetUnderline());
                    cell.Add(new Paragraph("Mr. Prst______ "));
                    p = new Paragraph();
                    p.Add(new Text("Dt. "));
                    p.Add(new Text(stokFaturaBilgi.GelenFaturaTarih.ToString("dd.MM.yyyy")));
                    cell.Add(p);
                    cell.SetPadding(5);
                    cell.SetWidth(100);
                    table.AddCell(cell);

                    cell = new Cell(2, 4);
                    cell.Add(new Paragraph("Protokol Dr.  "));
                    cell.SetBorderTop(Border.NO_BORDER);
                    cell.SetBorderRight(Border.NO_BORDER);
                    cell.SetTextAlignment(TextAlignment.RIGHT);
                    table.AddCell(cell);

                    cell = new Cell(2, 6);
                    p = new Paragraph();
                    p.Add(new Text("Nr. "));
                    p.Add(new Text(stokFaturaBilgi.BakanlikProtokolNo).SetUnderline());
                    p.Add(new Text("  Dt. "));
                    p.Add(new Text(DateTime.Now.ToString("dd.MM.yyyy")).SetUnderline());
                    cell.Add(p);
                    cell.SetBorderTop(Border.NO_BORDER);
                    cell.SetBorderLeft(Border.NO_BORDER);
                    cell.SetTextAlignment(TextAlignment.CENTER);
                    table.AddCell(cell);

                    cell = new Cell(3, 2);
                    cell.Add(new Paragraph("Emri,mbiemri"));
                    cell.Add(new Paragraph("Pers.Autorizaur"));
                    cell.SetPadding(1);
                    table.AddCell(cell);

                    cell = new Cell(3, 4);
                    cell.Add(new Paragraph(""));
                    cell.Add(new Paragraph(""));
                    cell.SetWidth(100);
                    table.AddCell(cell);

                    cell = new Cell(3, 2);
                    cell.Add(new Paragraph("Lloji e targa"));
                    cell.Add(new Paragraph("Mjeti transp."));
                    cell.SetPadding(5);
                    table.AddCell(cell);

                    cell = new Cell(3, 4);
                    cell.Add(new Paragraph(""));
                    cell.Add(new Paragraph(""));
                    cell.SetWidth(100);
                    table.AddCell(cell);
                    TableHeaderEventHandler handler = new TableHeaderEventHandler(document, table, false);
                    pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, handler);


                    #endregion Header

                    #region Footer
                    table = new Table(new float[] { 1, 1, 1, 1, 1 }).UseAllAvailableWidth();
                    table.SetFontSize(10);
                    table.SetTextAlignment(TextAlignment.CENTER);
                    cell = new Cell(2, 1);
                    cell.Add(new Paragraph("Emri,mbiemri"));
                    cell.Add(new Paragraph("e nënshkrimi"));
                    table.AddCell(cell);

                    cell = new Cell(1, 1);
                    cell.Add(new Paragraph("Magazinieri"));
                    table.AddCell(cell);

                    cell = new Cell(1, 1);
                    cell.Add(new Paragraph("Marrësi në dorëzim"));
                    table.AddCell(cell);

                    cell = new Cell(1, 1);
                    cell.Add(new Paragraph("Transportuesi"));
                    table.AddCell(cell);

                    cell = new Cell(1, 1);
                    cell.Add(new Paragraph("Llogaritari"));
                    table.AddCell(cell);

                    cell = new Cell(1, 1);
                    cell.Add(new Paragraph(""));
                    table.AddCell(cell);

                    cell = new Cell(1, 1);
                    cell.Add(new Paragraph(""));
                    table.AddCell(cell);

                    cell = new Cell(1, 1);
                    cell.Add(new Paragraph(""));
                    table.AddCell(cell);

                    cell = new Cell(1, 1);
                    cell.Add(new Paragraph(""));
                    table.AddCell(cell);

                    handler = new TableHeaderEventHandler(document, table, true);
                    pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, handler);

                    #endregion Footer


                    table = new Table(widths, true);
                    table.SetTextAlignment(TextAlignment.CENTER);

                    cell = new Cell();
                    cell.Add(new Paragraph("Nr.").SetFont(bold));
                    table.AddHeaderCell(cell);

                    cell = new Cell();
                    cell.Add(new Paragraph("Emërtimi i mallit").SetFont(bold));
                    table.AddHeaderCell(cell);

                    cell = new Cell();
                    cell.Add(new Paragraph("Njësia").SetFont(bold));
                    table.AddHeaderCell(cell);

                    cell = new Cell();
                    cell.Add(new Paragraph("Sasia").SetFont(bold));
                    table.AddHeaderCell(cell);
                    cell = new Cell();
                    cell.Add(new Paragraph("Çmimi").SetFont(bold));
                    table.AddHeaderCell(cell);

                    cell = new Cell();
                    cell.Add(new Paragraph("Vlefta").SetFont(bold));
                    table.AddHeaderCell(cell);

                    cell = new Cell();
                    cell.Add(new Paragraph("Exp.Date").SetFont(bold));
                    table.AddHeaderCell(cell);




                    decimal toplamTutar = 0;
                    int i = 1;
                    //for(int k = 0; k < 20; k++)
                    //{
                    foreach (var item in stokFaturaDetay)
                    {
                        cell = new Cell().Add(new Paragraph(i.ToString()));
                        table.AddCell(cell);
                        cell = new Cell().Add(new Paragraph(item.StokAd).SetTextAlignment(TextAlignment.LEFT));
                        table.AddCell(cell);
                        cell = new Cell().Add(new Paragraph(item.AnaBirimN));
                        table.AddCell(cell);
                        cell = new Cell().Add(new Paragraph(((int)item.EklenenStok).ToString()));
                        table.AddCell(cell);
                        cell = new Cell().Add(new Paragraph(((int)item.BirimFiyat).ToString()));
                        table.AddCell(cell);
                        cell = new Cell().Add(new Paragraph(((int)(item.EklenenStok * item.BirimFiyat)).ToString()));
                        table.AddCell(cell);
                        cell = new Cell().Add(new Paragraph(item.SonKulTarih.ToString("dd.MM.yyyy")));
                        table.AddCell(cell);
                        i++;
                        toplamTutar += (item.EklenenStok * item.BirimFiyat);
                    }
                    //}







                    cell = new Cell(1, 5).Add(new Paragraph(""));
                    cell.SetBorder(Border.NO_BORDER);
                    table.AddCell(cell);

                    cell = new Cell(1, 2);
                    p = new Paragraph(new Text("Total :    "));
                    p.Add(new Text(((int)toplamTutar).ToString()).SetUnderline().SetFontSize(13));
                    cell.Add(p);
                    cell.SetTextAlignment(TextAlignment.CENTER);
                    cell.SetBorder(Border.NO_BORDER);
                    table.AddCell(cell);



                    document.Add(table);
                    table.Flush();
                    table.Complete();
                    document.Close();
                    byte[] bytes = memoryStream.ToArray();
                    memoryStream.Close();

                    //var str = String.Join(",", bytes);
                    //var result = Convert.ToBase64String(bytes, Base64FormattingOptions.InsertLineBreaks);


                    return new SuccessDataResult<byte[]>(bytes);
                }
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<byte[]>();
            }
        }

        public IDataResult<byte[]> IlacCikisPdf(int IslemNo)
        {
            throw new NotImplementedException();
        }

        public IDataResult<byte[]> StokCikisPdf(int IslemNo)
        {
            // var stokCikis = _stokCikisService.GetByIslemNo(IslemNo).Data;
            var stokCikisDetay = _stokCikisService.GetCikisDetay(IslemNo).Data;
            try
            {
                using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                {
                    PdfFont font = PdfFontFactory.CreateFont(FontConstants.TIMES_ROMAN);
                    PdfFont bold = PdfFontFactory.CreateFont(FontConstants.TIMES_BOLD);
                    PdfWriter writer = new PdfWriter(memoryStream);
                    PdfDocument pdf = new PdfDocument(writer);
                    Document document = new Document(pdf);
                    document.SetMargins(150, 35, 70, 35);

                    float[] widths = { 1, 5, 2, 2, 2, 2, 2 };

                    #region Header
                    Table table = new Table(widths, true).UseAllAvailableWidth();
                    Cell cell;

                    cell = new Cell(3, 5);
                    cell.Add(new Paragraph("Spitali   __________________"));
                    cell.Add(new Paragraph("Pavioni __________________"));

                    Paragraph p = new Paragraph();
                    p.Add(new Text("Data     "));
                    p.Add(new Text(DateTime.Now.ToString("  dd / MM / yyyy   ")).SetUnderline());
                    cell.Add(p);
                    cell.SetBorder(Border.NO_BORDER);
                    table.AddCell(cell);


                    cell = new Cell(3, 3);
                    cell.Add(new Paragraph("Format 105"));
                    cell.Add(new Paragraph("A.N.______"));
                    cell.SetBorder(Border.NO_BORDER);
                    table.AddCell(cell);

                    cell = new Cell(3, 7);
                    cell.Add(new Paragraph(""));
                    cell.Add(new Paragraph("BLLOK TËRHEQJE BARNASH").SetTextAlignment(TextAlignment.CENTER).SetFont(bold).SetFontSize(20));
                    cell.SetPadding(15);
                    cell.SetBorder(Border.NO_BORDER);
                    table.AddCell(cell);

                    TableHeaderEventHandler handler = new TableHeaderEventHandler(document, table, false);
                    pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, handler);

                    #endregion Header

                    #region Footer

                    table = new Table(widths).UseAllAvailableWidth();
                    cell = new Cell(0, 7);
                    cell.Add(new Paragraph(""));
                    cell.SetBorder(Border.NO_BORDER);
                    cell.SetPadding(15);
                    table.AddCell(cell);

                    cell = new Cell(0, 2);
                    cell.Add(new Paragraph("Dorëzuesi"));
                    cell.Add(new Paragraph(""));
                    cell.Add(new Paragraph("(K/infermieri)"));
                    cell.SetBorder(Border.NO_BORDER);
                    cell.SetTextAlignment(TextAlignment.CENTER);
                    table.AddCell(cell);

                    cell = new Cell(3, 3);
                    cell.Add(new Paragraph("U paShefi i pavionit"));
                    cell.SetBorder(Border.NO_BORDER);
                    cell.SetTextAlignment(TextAlignment.CENTER);
                    table.AddCell(cell);

                    cell = new Cell(5, 2);
                    cell.Add(new Paragraph("Marrësi"));
                    cell.Add(new Paragraph(""));
                    cell.Add(new Paragraph("(Farmazisti)"));
                    cell.SetBorder(Border.NO_BORDER);
                    cell.SetTextAlignment(TextAlignment.CENTER);
                    table.AddCell(cell);

                    handler = new TableHeaderEventHandler(document, table, true);
                    pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, handler);

                    #endregion Footer

                    table = new Table(widths, true);
                    table.SetTextAlignment(TextAlignment.CENTER);

                    cell = new Cell();
                    cell.Add(new Paragraph("Nr.").SetFont(bold));
                    table.AddHeaderCell(cell);

                    cell = new Cell();
                    cell.Add(new Paragraph("Lloji i barnave").SetFont(bold));
                    table.AddHeaderCell(cell);

                    cell = new Cell();
                    cell.Add(new Paragraph("Njësia e mates").SetFont(bold));
                    table.AddHeaderCell(cell);

                    cell = new Cell();
                    cell.Add(new Paragraph("Sasia e kërkuar").SetFont(bold));
                    table.AddHeaderCell(cell);

                    cell = new Cell();
                    cell.Add(new Paragraph("Sasia e dorëzuar").SetFont(bold));
                    table.AddHeaderCell(cell);

                    cell = new Cell();
                    cell.Add(new Paragraph("Çmimi").SetFont(bold));
                    table.AddHeaderCell(cell);

                    cell = new Cell();
                    cell.Add(new Paragraph("Vlefta").SetFont(bold));
                    table.AddHeaderCell(cell);
                    int i = 1;
                    foreach (var item in stokCikisDetay)
                    {
                        cell = new Cell().Add(new Paragraph(i + " )"));
                        table.AddCell(cell);
                        cell = new Cell().Add(new Paragraph(item.StokAd).SetTextAlignment(TextAlignment.LEFT));
                        table.AddCell(cell);
                        cell = new Cell().Add(new Paragraph(item.AnaBirimN));
                        table.AddCell(cell);
                        cell = new Cell().Add(new Paragraph(Convert.ToInt32(item.Istenilen).ToString()));
                        table.AddCell(cell);
                        cell = new Cell().Add(new Paragraph(Convert.ToInt32(item.Verilen).ToString()));
                        table.AddCell(cell);
                        cell = new Cell().Add(new Paragraph(""));
                        table.AddCell(cell);
                        cell = new Cell().Add(new Paragraph(""));
                        table.AddCell(cell);
                        i++;
                    }

                    document.Add(table);
                    table.Flush();
                    table.Complete();

                    document.Close();
                    byte[] bytes = memoryStream.ToArray();
                    memoryStream.Close();

                    //var str = String.Join(",", bytes);
                    //var result = Convert.ToBase64String(bytes, Base64FormattingOptions.InsertLineBreaks);


                    return new SuccessDataResult<byte[]>(bytes);
                }
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<byte[]>();
            }

        }

        public IDataResult<byte[]> BakanlikFormatFatura(int PkFatura)
        {
            var stokFaturaBilgi = _stokFaturaService.GetFaturaBilgi(PkFatura).Data;
            var stokFaturaDetay = _stokFaturaService.GetFaturaDetay(PkFatura).Data;
            try
            {
                using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                {
                    PdfFont font = PdfFontFactory.CreateFont(FontConstants.TIMES_ROMAN);
                    PdfFont bold = PdfFontFactory.CreateFont(FontConstants.TIMES_BOLD);
                    PdfWriter writer = new PdfWriter(memoryStream);
                    PdfDocument pdf = new PdfDocument(writer);
                    PageSize pageSize = PageSize.A4;
                    Document document = new Document(pdf, pageSize);
                   
                    document.SetFont(font);
                    float[] widths = { 1, 5, 2, 2, 2, 2, 2 };


                    Table table = new Table(widths, true);

                    Cell cell = new Cell(1, 7);
                    Image img = new Image(ImageDataFactory.Create(IMAGEH));
                    img.SetWidth(UnitValue.CreatePercentValue(100));
                    cell.Add(img);
                    cell.SetBorder(Border.NO_BORDER);

                    table.AddCell(cell);
                    cell = new Cell(1, 7);
                    cell.SetBorder(Border.NO_BORDER);
                    cell.SetTextAlignment(TextAlignment.CENTER);
                    cell.SetPadding(20);
                    cell.Add(new Paragraph("PROCES - VERBAL").SetFont(bold));

                    table.AddCell(cell);
                    cell = new Cell(1, 7);
                    cell.SetBorder(Border.NO_BORDER);
                    cell.SetTextAlignment(TextAlignment.JUSTIFIED);
                    Paragraph p = new Paragraph();
                    p.Add(new Text("Proces-verbal i mbajtur sot më "));
                    p.Add(new Text(DateTime.Now.ToString("dd.MM.yyyy")).SetUnderline());
                    p.Add(new Text(" në farmacinë e Spitalit Rajonal “ Memorial” për pritjen, administrimin " +
                        "dhe dokumentimin e medikamenteve , materialeve të mjekimit dhe kite reagentë, mbështetur " +
                        "në ligjin nr. 10296, datë 07.07.2010, “Për Menaxhimin Financiar dhe Kontrollin”, udhëzimin e " +
                        "Ministrisë së Financave nr. 30, datë 27.12.2011 “Për Menaxhimin e Aktiveve në Njësitë e Sektorit " +
                        "Publik” , si dhe urdhërin nr.118. datë 28.05.2021 të Drejtorisë së Spitalit Rajonal “Memorial” Fier “"));
                    cell.Add(p);

                    cell.Add(new Paragraph(""));
                    cell.Add(new Paragraph(""));
                    p = new Paragraph();
                    p.Add(new Text("Komisioni i pritjes së mallit bëri kontrollin e sasisë, cilësisë dhe specifikimeve teknike sipas Urdherit nr. " + stokFaturaBilgi.GelenFaturaNo + " "));
                    p.Add(new Text("date " + stokFaturaBilgi.GelenFaturaTarih.ToString("dd.MM.yyyy") + " "));
                    p.Add(new Text(",kalim medikamentesh nga " + stokFaturaBilgi.GelenBilgiUnvan));
                    p.Add(new Text(" per ne Spitalin Rajonal Memorial me flete dalje nr " + stokFaturaBilgi.FaturaNo));
                    p.Add(new Text(" date  " + stokFaturaBilgi.FaturaTarih.ToString("dd.MM.yyyy")));
                    cell.Add(p);

                    cell.SetPadding(20);
                    table.AddCell(cell);
                    document.Add(table);

                    table = new Table(widths, true).UseAllAvailableWidth();
                    table.SetTextAlignment(TextAlignment.CENTER);

                    cell = new Cell();
                    cell.Add(new Paragraph("Nr.").SetFont(bold));
                    table.AddHeaderCell(cell);

                    cell = new Cell();
                    cell.Add(new Paragraph("Emërtimi").SetFont(bold));
                    table.AddHeaderCell(cell);

                    cell = new Cell();
                    cell.Add(new Paragraph("Njësia").SetFont(bold));
                    table.AddHeaderCell(cell);

                    cell = new Cell();
                    cell.Add(new Paragraph("Sasia").SetFont(bold));
                    table.AddHeaderCell(cell);
                    cell = new Cell();
                    cell.Add(new Paragraph("Çmimi").SetFont(bold));
                    table.AddHeaderCell(cell);

                    cell = new Cell();
                    cell.Add(new Paragraph("Datë prodhimi").SetFont(bold));
                    table.AddHeaderCell(cell);

                    cell = new Cell();
                    cell.Add(new Paragraph("Datë skadence").SetFont(bold));
                    table.AddHeaderCell(cell);




                    int i = 1;
                    //for(int k = 0; k < 20; k++)
                    //{
                    foreach (var item in stokFaturaDetay)
                    {
                        cell = new Cell().Add(new Paragraph(i.ToString()));
                        table.AddCell(cell);
                        cell = new Cell().Add(new Paragraph(item.StokAd).SetTextAlignment(TextAlignment.LEFT));
                        table.AddCell(cell);
                        cell = new Cell().Add(new Paragraph(item.AnaBirimN));
                        table.AddCell(cell);
                        cell = new Cell().Add(new Paragraph(((int)item.EklenenStok).ToString()));
                        table.AddCell(cell);
                        cell = new Cell().Add(new Paragraph(((int)item.BirimFiyat).ToString()));
                        table.AddCell(cell);
                        cell = new Cell().Add(new Paragraph(""));
                        table.AddCell(cell);
                        cell = new Cell().Add(new Paragraph(item.SonKulTarih.ToString("MM.yyyy")));
                        table.AddCell(cell);
                        i++;
                    }
                    //}








                    cell = new Cell(1, 7);
                    cell.Add(new Paragraph(new Text("Malli bëhet hyrje në farmacinë e spitalit me fletëhyrjedatë  " + stokFaturaBilgi.FaturaTarih.ToString("dd.MM.yyyy"))));
                    cell.SetBorder(Border.NO_BORDER);
                    cell.SetTextAlignment(TextAlignment.LEFT);
                    cell.SetPadding(10);

                    table.AddCell(cell);


                    document.Add(table);
                    table.Flush();
                    table.Complete();

                    table = new Table(new float[] { 1, 1, 1 }, true).UseAllAvailableWidth();
                    table.SetFont(bold);
                    table.SetPaddingTop(40);

                    cell = new Cell(1, 3);
                    cell.Add(new Paragraph(""));
                    cell.Add(new Paragraph(new Text("KOMISIONI").SetUnderline()));
                    cell.SetBorder(Border.NO_BORDER);
                    cell.SetTextAlignment(TextAlignment.CENTER);
                    table.AddCell(cell);

                    cell = new Cell();
                    cell.Add(new Paragraph(new Text("UMMIYE YILMAZ")));
                    cell.SetBorder(Border.NO_BORDER);
                    cell.SetTextAlignment(TextAlignment.CENTER);
                    table.AddCell(cell);

                    cell = new Cell();
                    cell.Add(new Paragraph(new Text("KADRIYE ATILA")));
                    cell.SetBorder(Border.NO_BORDER);
                    cell.SetTextAlignment(TextAlignment.CENTER);
                    table.AddCell(cell);

                    cell = new Cell();
                    cell.Add(new Paragraph(new Text("AURORA KOLA")));
                    cell.SetBorder(Border.NO_BORDER);
                    cell.SetTextAlignment(TextAlignment.CENTER);
                    table.AddCell(cell);

                    document.Add(table);

                    document.Close();
                    byte[] bytes = memoryStream.ToArray();
                    memoryStream.Close();

                    //var str = String.Join(",", bytes);
                    //var result = Convert.ToBase64String(bytes, Base64FormattingOptions.InsertLineBreaks);


                    return new SuccessDataResult<byte[]>(bytes);
                }
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<byte[]>();
            }
        }

        private class TableHeaderEventHandler : IEventHandler
        {
            private Table table;
            private float tableHeight;
            private Document doc;
            private bool bottom;

            public TableHeaderEventHandler(Document doc, Table table, bool bottom)
            {
                this.doc = doc;
                this.table = table;
                this.bottom = bottom;

                TableRenderer renderer = (TableRenderer)table.CreateRendererSubTree();
                renderer.SetParent(new DocumentRenderer(doc));

                // Simulate the positioning of the renderer to find out how much space the header table will occupy.
                LayoutResult result = renderer.Layout(new LayoutContext(new LayoutArea(0, PageSize.A4)));
                tableHeight = result.GetOccupiedArea().GetBBox().GetHeight();
            }

            public void HandleEvent(Event currentEvent)
            {
                PdfDocumentEvent docEvent = (PdfDocumentEvent)currentEvent;
                PdfDocument pdfDoc = docEvent.GetDocument();
                PdfPage page = docEvent.GetPage();
                PdfCanvas canvas = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), pdfDoc);
                PageSize pageSize = pdfDoc.GetDefaultPageSize();
                float coordX = pageSize.GetX() + doc.GetLeftMargin();
                float coordY = this.bottom ? pageSize.GetBottom() + (doc.GetBottomMargin() / 2) : pageSize.GetTop() - doc.GetTopMargin();
                float width = pageSize.GetWidth() - doc.GetRightMargin() - doc.GetLeftMargin();
                float height = GetTableHeight();
                Rectangle rect = new Rectangle(coordX, coordY, width, height);

                new Canvas(canvas, rect)
                    .Add(table)
                    .Close();
            }

            public float GetTableHeight()
            {
                return tableHeight;
            }
        }

        private class PageBorderEventHandler : IEventHandler
        {

            public void HandleEvent(Event currentEvent)
            {
                PdfDocumentEvent docEvent = (PdfDocumentEvent)currentEvent;
                PdfCanvas canvas = new PdfCanvas(docEvent.GetPage());
                var pageSize = docEvent.GetPage().GetPageSize();
                Rectangle rect = new Rectangle(pageSize.GetX() + 32, pageSize.GetY() + 32, pageSize.GetWidth() - 64, pageSize.GetHeight() - 37);
                //Rectangle rect = new Rectangle(pageSize.GetWidth() - 40, pageSize.GetHeight());

                canvas.Rectangle(rect)
                      .Stroke();
            }
        }
        private class PageBackgroundEventHandler : IEventHandler
        {
            private string imageUrl;

            public PageBackgroundEventHandler(string imageUrl)
            {
                this.imageUrl = imageUrl;
            }
            public void HandleEvent(Event currentEvent)
            {
                PdfDocumentEvent docEvent = (PdfDocumentEvent)currentEvent;

                ImageData image = ImageDataFactory.Create(IMAGE);
                PdfCanvas canvas = new PdfCanvas(docEvent.GetPage());
                var pageSize = docEvent.GetPage().GetPageSize();
                canvas.SaveState();
                PdfExtGState state = new PdfExtGState().SetFillOpacity(0.4f);
                canvas.SetExtGState(state);
                Rectangle rect = new Rectangle(pageSize.GetWidth() / 3, pageSize.GetHeight() / 2.5f, 200, 200);
                canvas.AddImageFittedIntoRectangle(image, rect, false);
                canvas.RestoreState();

            }
        }



    }
}
