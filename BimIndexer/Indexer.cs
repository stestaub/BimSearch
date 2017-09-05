using System;
using Xbim.Ifc;
using Xbim.Ifc2x3.Interfaces;

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
				var project = model.Instances.FirstOrDefault<IIfcProject>();

				var document = new {
					Name = project.Name?.Value,
					LongName = project.LongName?.Value,
					Description = project.Description?.Value
				};
				var indexResponse = this.connection.client.Index<byte[]>("bim_projects", "project", "2", document);
				byte[] responseBytes = indexResponse.Body;
			}
		}
	}
}

