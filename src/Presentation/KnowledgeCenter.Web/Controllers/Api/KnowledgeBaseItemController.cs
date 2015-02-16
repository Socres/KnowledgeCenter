namespace KnowledgeCenter.Web.Controllers.Api
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using KnowledgeCenter.Domain.Core.Interfaces;
    using KnowledgeCenter.Domain.Core.Models;

    [RoutePrefix("api/KnowledgeBase")]
    public class KnowledgeBaseItemController : ApiController
    {
        private readonly IKnowledgeBaseService _knowledgeBaseService;

        /// <summary>
        /// Initializes a new instance of the <see cref="KnowledgeBaseItemController"/> class.
        /// </summary>
        /// <param name="knowledgeBaseService">The knowledge base service.</param>
        public KnowledgeBaseItemController(IKnowledgeBaseService knowledgeBaseService)
        {
            _knowledgeBaseService = knowledgeBaseService;
        }

        /// <summary>
        /// GET: api/KnowledgeBaseItem/2
        /// Gets the knowledgebase root items.
        /// </summary>
        /// <returns></returns>
        [Route("")]
        public IEnumerable<KnowledgeBaseItem> Get()
        {
            return _knowledgeBaseService.GetItems(null);
        }

        /// <summary>
        /// GET: api/KnowledgeBaseItem/2
        /// Gets the knowledgebaseItem's child items.
        /// </summary>
        /// <param name="parentId">The identifier.</param>
        /// <returns></returns>
        [Route("{parentId:Guid}")]
        public IEnumerable<KnowledgeBaseItem> Get(Guid parentId)
        {
            return _knowledgeBaseService.GetItems(parentId);
        }

        /// <summary>
        /// GET: api/KnowledgeBaseItem/GetRootParent/2
        /// Gets the root parent.
        /// </summary>
        /// <param name="childId">The identifier.</param>
        /// <returns></returns>
        [Route("RootTree/{childId}")]
        [HttpGet]
        public KnowledgeBaseItem GetRootTreeFromChild(Guid childId)
        {
            return _knowledgeBaseService.GetRootTreeFromChild(childId);
        }

        /// <summary>
        /// POST: api/KnowledgeBaseItem
        /// Creates a new <see cref="KnowledgeBaseItem"/>.
        /// </summary>
        /// <param name="item">The item.</param>
        [Route("")]
        public HttpResponseMessage Post([FromBody]KnowledgeBaseItem item)
        {
            //item = _knowledgeBaseService.AddItem(item);
            var response = Request.CreateResponse(HttpStatusCode.Created, item);

            string uri = Url.Link("DefaultApi", new { id = item.DomainId });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        /// <summary>
        /// Updates an existing <see cref="KnowledgeBaseItem" />.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="item">The item.</param>
        /// <exception cref="System.Web.Http.HttpResponseException"></exception>
        [Route("")]
        public void Put(int id, [FromBody]KnowledgeBaseItem item)
        {
            //item.Id = id;
            //if (!_knowledgeBaseService.UpdateItem(item))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }
    }
}
