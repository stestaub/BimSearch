using System;
using Xbim.Ifc2x3.Kernel;

namespace BimIndexer
{
	public class ProductSummary
	{
		public string Name { get; set; }
		public string IfcType { get; set; }
		public string PropertySummary { get; set; }
		public string DocumentId { get; set; }

		public ProductSummary(string documentId)
		{
			this.DocumentId = documentId;
		}

		public ProductSummary (IfcProduct product, string documentId)
		{
			this.DocumentId = documentId;
			this.ParseProduct(product);
		}

		public void ParseProduct(IfcProduct product) 
		{
			this.Name = product.FriendlyName;
			this.IfcType = product.GetType().Name;
			this.PropertySummary = this.CreatePropertySummary(product);
		}

		private string CreatePropertySummary(IfcProduct product) 
		{
			var summary = "";
			foreach(var pSet in product.PropertySets) 
			{
				Console.WriteLine(pSet.ToString());
			}
			return summary;
		}
	}
}

