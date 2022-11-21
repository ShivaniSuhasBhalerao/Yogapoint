using System;
using System.Collections.Generic;
using System.Text;
using TestLoginYogapoint.Localization;
using Volo.Abp.Application.Services;

namespace TestLoginYogapoint;

/* Inherit your application services from this class.
 */
public abstract class TestLoginYogapointAppService : ApplicationService
{
    protected TestLoginYogapointAppService()
    {
        LocalizationResource = typeof(TestLoginYogapointResource);
    }
}
