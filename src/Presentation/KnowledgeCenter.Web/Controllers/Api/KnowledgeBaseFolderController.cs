namespace KnowledgeCenter.Web.Controllers.Api
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using KnowledgeCenter.Domain.Core.Interfaces;
    using KnowledgeCenter.Domain.Core.Models;

    public class KnowledgeBaseFolderController : ApiController
    {
        private readonly IKnowledgeBaseService _knowledgeBaseService;

        /// <summary>
        /// Initializes a new instance of the <see cref="KnowledgeBaseFolderController"/> class.
        /// </summary>
        /// <param name="knowledgeBaseService">The knowledge base service.</param>
        public KnowledgeBaseFolderController(IKnowledgeBaseService knowledgeBaseService)
        {
            _knowledgeBaseService = knowledgeBaseService;
        }

        /// <summary>
        /// GET: api/KnowledgeBaseFolder/2
        /// Gets the knowledgebase root folders.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<KnowledgeBaseFolder> Get()
        {
            return _knowledgeBaseService.GetFolders(null);
        }

        /// <summary>
        /// GET: api/KnowledgeBaseFolder/2
        /// Gets the knowledgebase child folders.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public IEnumerable<KnowledgeBaseFolder> Get(int id)
        {
            return _knowledgeBaseService.GetFolders(id);
        }

        /// <summary>
        /// POST: api/KnowledgeBaseFolder
        /// Creates a new <see cref="KnowledgeBaseFolder"/>.
        /// </summary>
        /// <param name="item">The item.</param>
        public HttpResponseMessage Post([FromBody]KnowledgeBaseFolder item)
        {
            //item = _knowledgeBaseService.AddFolder(item);
            var response = Request.CreateResponse<KnowledgeBaseFolder>(HttpStatusCode.Created, item);

            string uri = Url.Link("DefaultApi", new { id = item.DomainId });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        /// <summary>
        /// Updates an existing <see cref="KnowledgeBaseFolder" />.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="item">The item.</param>
        /// <exception cref="System.Web.Http.HttpResponseException"></exception>
        public void Put(int id, [FromBody]KnowledgeBaseFolder item)
        {
            //item.Id = id;
            //if (!_knowledgeBaseService.UpdateFolder(item))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }
    }
}
