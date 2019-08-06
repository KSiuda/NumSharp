﻿/*
This file was generated by template ../NDArray.Elementwise.tt
In case you want to do some changes do the following 

1 ) adapt the tt file
2 ) execute powershell file "GenerateCode.ps1" on root level

*/

using System;

namespace NumSharp.Backends
{
    public partial class DefaultEngine
    {
        public override NDArray Sum(in NDArray nd, int axis, Type dtype, bool keepdims = false)
        {
            return Sum(nd, axis, dtype != null ? dtype.GetTypeCode() : default(NPTypeCode?), keepdims);
        }

        public override NDArray Sum(in NDArray nd, int? axis = null, NPTypeCode? typeCode = null, bool keepdims = false)
        {
            return ReduceAdd(nd, axis, keepdims, typeCode);
        }
    }
}
