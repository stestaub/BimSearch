using System;
using Xbim.Ifc;
using Xbim.Ifc2x3.Interfaces;
using Xbim.Ifc2x3.Kernel;

namespace BimIndexer
{
	public class Indexer
	{
		private ElasticSearchConnection connection;

		public Indexer (ElasticSearchConnection connetion)
		{
			this.connection = connetion;
		}

		public void IndexIfc(string filePath) 
		{
			using (var model = IfcStore.Open(filePath))
			{
				Console.WriteLine("Start Indexing model.....");
				var project = model.Instances.FirstOrDefault<IIfcProject>();

				var document = new {
					Name = project.Name?.Value,
					LongName = project.LongName?.Value,
					Description = project.Description?.Value,
					DocumentPath = filePath,
					DocumentId = project.GlobalId.Value
				};

				Console.WriteLine("Indexing Products for model {0}", document.LongName);
				this.IndexProducts(model, (string)document.DocumentId, "1");

				var indexResponse = this.connection.client.Index<byte[]>("bim_projects", "project", "2", document);
				byte[] responseBytes = indexResponse.Body;
			}
		}

		public void IndexProducts(IfcStore model, string documentId, string version) 
		{
			foreach(var instance in model.Instances.OfType<IfcProduct>()) 
			{
				var summary = new ProductSummary(instance, documentId);
				var indexResponse = this.connection.client.Index<byte[]>("bim_projects", "products", version, summary);
				byte[] responseBytes = indexResponse.Body;
			}
		}	
	}
}

