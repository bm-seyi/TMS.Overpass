using Microsoft.Extensions.Configuration;
using TMS.Overpass.Application.Extensions;
using TMS.Overpass.Infrastructure.Interfaces;

namespace TMS.Overpass.Infrastructure.Persistence.Context;

internal sealed class TmsContext(IConfiguration configuration) : DapperContext(configuration.GetRequiredConnectionString("TMS")), IDapperContext {}