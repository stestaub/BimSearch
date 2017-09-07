using System;
using Xbim.Ifc2x3.Kernel;
using System.Text;
using Xbim.Ifc2x3.PropertyResource;

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
			Console.Write(this.PropertySummary);
		}

		private string CreatePropertySummary(IfcProduct product) 
		{
			var summary = new StringBuilder();
			foreach(var pSet in product.PropertySets) 
			{
				foreach(var property in pSet.HasProperties)
				{
					IfcPropertySingleValue singleVal = property as IfcPropertySingleValue;
					if (singleVal == null) continue;
					summary.AppendLine(singleVal.Name.Value + ": " + singleVal.NominalValue.Value + " " + singleVal.Unit);
				}
			}
			return summary.ToString();
		}
	}
}

