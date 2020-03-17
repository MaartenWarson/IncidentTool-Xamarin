using System;
using System.Collections.Generic;
using System.Text;

namespace IncidentTool.Interfaces.Services.General
{
    public interface IDependencyResolver
    {
        object Resolve(Type typeName);
        T Resolve<T>();
    }
}
