using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using LogicaDeNegocio.Clases;
using LogicaDeNegocio.Clases.ClasesCompuestas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio.Servicios
{
	public static class ServiciosDeCreacionDePDF
	{
		public static void GenerarReporteDeInventario(List<Discrepancia> discrepancias, List<ObjetoDeInventario> objetosDeInventario, string direccion)
		{
			PdfWriter writer = new PdfWriter(direccion);
			PdfDocument pdf = new PdfDocument(writer);
			PageSize tamañoDePagina = new PageSize(842, 680);
			Document documento = new Document(pdf, tamañoDePagina);
			Table tablaDeObjetosDeInventario = new Table(new float[] { 1, 1, 1, 1, 1, 1 });
			Table tablaDeDiscrepancias = new Table(new float[] { 1, 1, 1, 1, 1, 1 });
			tablaDeDiscrepancias.SetWidth(UnitValue.CreatePercentValue(100)).SetTextAlignment(TextAlignment.CENTER).SetHorizontalAlignment(HorizontalAlignment.CENTER);
			tablaDeObjetosDeInventario.SetWidth(UnitValue.CreatePercentValue(100)).SetTextAlignment(TextAlignment.CENTER).SetHorizontalAlignment(HorizontalAlignment.CENTER);

			CrearHeadersDeDiscrepancias(tablaDeDiscrepancias);
			CrearHeadersDeObjetosDeInventario(tablaDeObjetosDeInventario);
			documento.Add(new Paragraph("Reporte de inventario de italia pizza"));
			documento.Add(new Paragraph("Fecha: " + DateTime.Now.ToString()));

			if (discrepancias.Count > 0)
			{
				documento.Add(new Paragraph("Reporte de discrepancias"));
				foreach (Discrepancia discrepancia in discrepancias)
				{
					AñadirDiscrepanciaATabla(tablaDeDiscrepancias, discrepancia);
				}
				documento.Add(tablaDeDiscrepancias);
			}
			else
			{
				documento.Add(new Paragraph("No hubo discrepancias"));
			}


			foreach (ObjetoDeInventario objetoDeInventario in objetosDeInventario)
			{
				AñadirObjetoDeInventarioATabla(tablaDeObjetosDeInventario, objetoDeInventario);
			}


			documento.Add(new Paragraph("Reporte de existencias"));
			documento.Add(tablaDeObjetosDeInventario);
			documento.Close();
		}
		private static void CrearHeadersDeDiscrepancias(Table tablaDeDiscrepancias)
		{
			PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

			Cell nombre = new Cell();
			nombre.SetFont(bold);
			nombre.Add(new Paragraph("Nombre"));
			Cell codigo = new Cell();
			codigo.SetFont(bold);
			codigo.Add(new Paragraph("Codigo"));
			Cell cantidadRegistrada = new Cell();
			cantidadRegistrada.SetFont(bold);
			cantidadRegistrada.Add(new Paragraph("Cantidad registrada"));
			Cell cantidadEsperada = new Cell();
			cantidadEsperada.SetFont(bold);
			cantidadEsperada.Add(new Paragraph("Cantidad esperada"));
			Cell costo = new Cell();
			costo.SetFont(bold);
			costo.Add(new Paragraph("Costo"));
			Cell unidadDeMedida = new Cell();
			unidadDeMedida.SetFont(bold);
			unidadDeMedida.Add(new Paragraph("Unidad de medida"));

			tablaDeDiscrepancias.AddHeaderCell(nombre);
			tablaDeDiscrepancias.AddHeaderCell(codigo);
			tablaDeDiscrepancias.AddHeaderCell(cantidadRegistrada);
			tablaDeDiscrepancias.AddHeaderCell(cantidadEsperada);
			tablaDeDiscrepancias.AddHeaderCell(costo);
			tablaDeDiscrepancias.AddHeaderCell(unidadDeMedida);
		} 
		private static void AñadirDiscrepanciaATabla(Table tablaDeDiscrepancias, Discrepancia discrepancia)
		{
			PdfFont helvetica = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
			Cell nombre = new Cell();
			nombre.SetFont(helvetica);
			nombre.Add(new Paragraph(discrepancia.Nombre));
			Cell codigo = new Cell();
			codigo.SetFont(helvetica);
			codigo.Add(new Paragraph(discrepancia.Codigo));
			Cell cantidadRegistrada = new Cell();
			cantidadRegistrada.SetFont(helvetica);
			cantidadRegistrada.Add(new Paragraph(discrepancia.CantidadRegistrada.ToString()));
			Cell cantidadEsperada = new Cell();
			cantidadEsperada.SetFont(helvetica);
			cantidadEsperada.Add(new Paragraph(discrepancia.CantidadEsperada.ToString()));
			Cell costo = new Cell();
			costo.SetFont(helvetica);
			costo.Add(new Paragraph(discrepancia.Codigo));
			Cell unidadDeMedida = new Cell();
			unidadDeMedida.SetFont(helvetica);
			unidadDeMedida.Add(new Paragraph(discrepancia.UnidadDeMedida.ToString()));
			if (discrepancia.CantidadEsperada < discrepancia.CantidadRegistrada)
			{
				cantidadRegistrada.SetBackgroundColor(ColorConstants.GREEN);
				cantidadEsperada.SetBackgroundColor(ColorConstants.GREEN);
			}
			else
			{
				cantidadRegistrada.SetBackgroundColor(ColorConstants.RED);
				cantidadEsperada.SetBackgroundColor(ColorConstants.RED);
			}

			tablaDeDiscrepancias.AddCell(nombre);
			tablaDeDiscrepancias.AddCell(codigo);
			tablaDeDiscrepancias.AddCell(cantidadRegistrada);
			tablaDeDiscrepancias.AddCell(cantidadEsperada);
			tablaDeDiscrepancias.AddCell(costo);
			tablaDeDiscrepancias.AddCell(unidadDeMedida);
		}
		private static void CrearHeadersDeObjetosDeInventario(Table tablaDeObjetosDeInventario)
		{
			PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

			Cell nombre = new Cell();
			nombre.SetFont(bold);
			nombre.Add(new Paragraph("Nombre"));
			Cell codigo = new Cell();
			codigo.SetFont(bold);
			codigo.Add(new Paragraph("Codigo"));
			Cell cantidadRegistrada = new Cell();
			cantidadRegistrada.SetFont(bold);
			cantidadRegistrada.Add(new Paragraph("Cantidad registrada"));
			Cell costo = new Cell();
			costo.SetFont(bold);
			costo.Add(new Paragraph("Costo"));
			Cell tipoDeProducto = new Cell();
			tipoDeProducto.SetFont(bold);
			tipoDeProducto.Add(new Paragraph("Tipo de Producto"));
			Cell unidadDeMedida = new Cell();
			unidadDeMedida.SetFont(bold);
			unidadDeMedida.Add(new Paragraph("Unidad de medida"));

			tablaDeObjetosDeInventario.AddHeaderCell(nombre);
			tablaDeObjetosDeInventario.AddHeaderCell(codigo);
			tablaDeObjetosDeInventario.AddHeaderCell(cantidadRegistrada);
			tablaDeObjetosDeInventario.AddHeaderCell(costo);
			tablaDeObjetosDeInventario.AddHeaderCell(tipoDeProducto);
			tablaDeObjetosDeInventario.AddHeaderCell(unidadDeMedida);
		}
		private static void AñadirObjetoDeInventarioATabla(Table tablaDeDiscrepancias, ObjetoDeInventario objetoDeInventario)
		{
			PdfFont helvetica = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
			Cell nombre = new Cell();
			nombre.SetFont(helvetica);
			nombre.Add(new Paragraph(objetoDeInventario.Nombre));
			Cell codigo = new Cell();
			codigo.SetFont(helvetica);
			codigo.Add(new Paragraph(objetoDeInventario.Codigo));
			Cell cantidadRegistrada = new Cell();
			cantidadRegistrada.SetFont(helvetica);
			cantidadRegistrada.Add(new Paragraph(objetoDeInventario.Cantidad.ToString()));
			Cell costo = new Cell();
			costo.SetFont(helvetica);
			costo.Add(new Paragraph(objetoDeInventario.Costo.ToString()));
			Cell tipoDeProdutco = new Cell();
			tipoDeProdutco.SetFont(helvetica);
			tipoDeProdutco.Add(new Paragraph(objetoDeInventario.TipoDeProducto.ToString()));
			Cell unidadDeMedida = new Cell();
			unidadDeMedida.SetFont(helvetica);
			unidadDeMedida.Add(new Paragraph(objetoDeInventario.UnidadDeMedida.ToString()));
			tablaDeDiscrepancias.AddCell(nombre);
			tablaDeDiscrepancias.AddCell(codigo);
			tablaDeDiscrepancias.AddCell(cantidadRegistrada);
			tablaDeDiscrepancias.AddCell(costo);
			tablaDeDiscrepancias.AddCell(tipoDeProdutco);
			tablaDeDiscrepancias.AddCell(unidadDeMedida);
		}
	}
}
