﻿using Luban.Job.Common.Defs;
using Luban.Job.Common.RawDefs;
using Luban.Job.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luban.Job.Common.Utils
{
    public static class ExternalTypeUtil
    {

        //protected void ResolveExternalType()
        //{
        //    if (!string.IsNullOrEmpty(_externalTypeName))
        //    {
        //        if (AssemblyBase.TryGetExternalType(_externalTypeName, out var type))
        //        {
        //            this.ExternalType = type;
        //        }
        //        else
        //        {
        //            throw new Exception($"enum:'{FullName}' 对应的 externaltype:{_externalTypeName} 不存在");
        //        }
        //    }
        //}

        public static string CsMapperToExternalType(DefTypeBase type)
        {
            var mapper = DefAssemblyBase.LocalAssebmly.GetExternalTypeMapper(type.FullName);
            return mapper != null ? mapper.TargetTypeName : type.CsFullName;
        }

        public static string CsCloneToExternal(DefTypeBase type, string src)
        {
            var mapper = DefAssemblyBase.LocalAssebmly.GetExternalTypeMapper(type.FullName);
            if (mapper == null)
            {
                return src;
            }
            if (string.IsNullOrWhiteSpace(mapper.CreateExternalObjectFunction))
            {
                throw new Exception($"type:{type.FullName} externaltype:{DefAssemblyBase.LocalAssebmly.GetExternalType(type.FullName)} lan:{mapper.Lan} selector:{mapper.Selector} 未定义 create_external_object_function 属性");
            }
            return $"{mapper.CreateExternalObjectFunction}({src})";
        }
    }
}
