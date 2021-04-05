using AutoMapper;
using CloudFileSystem.Application.V1.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;

namespace CloudFileSystem.Application.V1.Services
{
    /// <summary>
    /// The application service which handles passing all actions down to the other layers.
    /// </summary>
    /// <seealso cref="CloudFileSystem.Application.V1.Interfaces.IAppService" />
    public partial class AppService : IAppService
    {
        /// <summary>The logger</summary>
        private readonly ILogger<AppService> _logger;

        /// <summary>The mapper</summary>
        private readonly IMapper _mapper;

        /// <summary>The mediator</summary>
        private readonly IMediator _mediator;

        /// <summary>Initializes a new instance of the <see cref="AppService" /> class.</summary>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="mediator">The mediator.</param>
        /// <exception cref="System.ArgumentNullException">logger
        /// or
        /// mapper
        /// or
        /// mediator</exception>
        public AppService(ILogger<AppService> logger, IMapper mapper, IMediator mediator)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this._mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
    }
}
