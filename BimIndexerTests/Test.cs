using NUnit.Framework;
using System;
using BimIndexer;

namespace BimIndexerTests
{
	[TestFixture ()]
	public class Test
	{
		[Test ()]
		public void TestCase ()
		{
			var connection = new ElasticSearchConnection();
			var indexer = new Indexer(connection);

			indexer.IndexIfc("Resources/02_BIMcollab_Example_STR.ifc");

			var searchResponse = connection.client.Search<string>("bim_projects", "project", new
				{
					query = new
					{
						query_string = new
						{
							query = "Revit"
						}
					}
				});

			var successful = searchResponse.Success;
			var responseJson = searchResponse.Body;

		}
	}
}

