using TestLoginYogapoint.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace TestLoginYogapoint.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class TestLoginYogapointController : AbpControllerBase
{
    protected TestLoginYogapointController()
    {
        LocalizationResource = typeof(TestLoginYogapointResource);
    }
}
