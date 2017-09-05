using System;
using Elasticsearch.Net;

namespace BimIndexer
{
	public class ElasticSearchConnection
	{

		public ElasticLowLevelClient client { get; }
		
		public ElasticSearchConnection ()
		{
			var settings = new ConnectionConfiguration(new Uri("http://localhost:9200"))
				.RequestTimeout(TimeSpan.FromMinutes(2));

			this.client = new ElasticLowLevelClient(settings);
		}
	}
}

