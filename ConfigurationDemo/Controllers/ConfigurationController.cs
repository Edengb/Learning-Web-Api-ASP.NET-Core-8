using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ConfigurationDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController(IConfiguration configuration) : ControllerBase
    {
        [HttpGet]
        [Route("my-key")]
        public ActionResult GetMyKey()
        {
            var mykey = configuration["MyKey"];
            return Ok(mykey);
        }

        [HttpGet("database-configuration")]
        public ActionResult GetDatabaseConfiguration()
        {
            var type = configuration["Database:Type"];
            var connectionString = configuration["Database:ConnectionString"];

            return Ok(new { Type = type, ConnectionString = connectionString });
        }

        [HttpGet]
        [Route("database-configuration-with-bind-1")]
        public ActionResult GetDatabaseConfigurationWithBind1()
        {
            DatabaseOption databaseOption = new DatabaseOption();
            configuration.GetSection(DatabaseOption.SectioName).Bind(databaseOption);
            return Ok(new { databaseOption.Type, databaseOption.ConnectionString });
        }

        [HttpGet]
        [Route("database-configuration-with-bind-2")]
        public ActionResult GetDatabaseConfigurationWithBind2()
        {
            DatabaseOption databaseOption = new DatabaseOption();
            configuration.Bind(DatabaseOption.SectioName, databaseOption);
            return Ok(new { databaseOption.Type, databaseOption.ConnectionString });
        }

        [HttpGet]
        [Route("database-configuration-with-generic-type")]
        public ActionResult GetDatabaseConfigurationWithGenericType()
        {
            DatabaseOption databaseOption = configuration.GetSection(DatabaseOption.SectioName).Get<DatabaseOption>();

            return Ok(new { databaseOption.Type, databaseOption.ConnectionString });
        }

        [HttpGet]
        [Route("database-configuration-with-ioptions")]
        public ActionResult GetDatabaseConfigurationWithIOptions([FromServices] IOptions<DatabaseOption> options)
        {
            var databaseOption = options.Value;
            return Ok(new { databaseOption.Type, databaseOption.ConnectionString });
        }

        [HttpGet("database-configuration-with-ioptions-snapshot")]
        public ActionResult GetDatabaseConfigurationWithIOptionsSnapshot([FromServices] IOptionsSnapshot<DatabaseOption> options)
        {
            var databaseOption = options.Value;
            return Ok(new { databaseOption.Type, databaseOption.ConnectionString });
        }

        [HttpGet]
        [Route("database-configuration-with-ioptions-monitor")]
        public ActionResult GetDatabaseConfigurationWithIOptionsMonitor([FromServices] IOptionsMonitor<DatabaseOption> options)
        {
            var databaseOption = options.CurrentValue;
            return Ok(new { databaseOption.Type, databaseOption.ConnectionString });
        }

        [HttpGet]
        [Route("database-configuration-with-named-options")]
        public ActionResult GetDatabaseConfigurationWithNamedOptions([FromServices] IOptionsSnapshot<DatabaseOptionsNamed> options)
        {
            var systemDatabaseOption = options.Get(DatabaseOptionsNamed.SystemDatabaseSectionName);
            var businessDatabaseOption = options.Get(DatabaseOptionsNamed.BusinessDatabaseSectionName);
            return Ok(new { SystemDatabaseOption = systemDatabaseOption, BusinessDatabaseOption = businessDatabaseOption });
        }
    }
}
