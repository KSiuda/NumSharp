﻿using System;
using NumSharp.Utilities;

namespace NumSharp.Backends
{
    public partial class DefaultEngine
    {
        public override NDArray ReduceAdd(in NDArray arr, int? axis_, bool keepdims = false, NPTypeCode? typeCode = null)
        {
            //in order to iterate an axis:
            //consider arange shaped (1,2,3,4) when we want to summarize axis 1 (2nd dimension which its value is 2)
            //the size of the array is [1, 2, n, m] all shapes after 2nd multiplied gives size
            //the size of what we need to reduce is the size of the shape of the given axis (shape[axis])
            var shape = arr.Shape;
            if (shape.IsEmpty)
                return arr;

            if (shape.IsScalar || shape.size == 1 && shape.dimensions.Length == 1)
            {
                var r = typeCode.HasValue ? Cast(arr, typeCode.Value, true) : arr.Clone();
                if (keepdims)
                    r.Storage.ExpandDimension(0);
                else if (!r.Shape.IsScalar && r.Shape.size == 1 && r.ndim == 1)
                    r.Storage.Reshape(Shape.Scalar);

                return r;
            }

            if (axis_ == null)
            {
                var r = NDArray.Scalar(sum_elementwise(arr, typeCode));
                if (keepdims)
                    r.Storage.ExpandDimension(0);
                else if (!r.Shape.IsScalar && r.Shape.size == 1 && r.ndim == 1)
                    r.Storage.Reshape(Shape.Scalar);

                return r;
            }

            var axis = axis_.Value;
            while (axis < 0)
                axis = arr.ndim + axis; //handle negative axis

            if (axis >= arr.ndim)
                throw new ArgumentOutOfRangeException(nameof(axis));

            if (shape[axis] == 1)
            {
                //if the given div axis is 1 and can be squeezed out.
                if (keepdims)
                    return new NDArray(arr.Storage.Alias());
                return np.squeeze_fast(arr, axis);
            }

            //handle keepdims
            Shape axisedShape = Shape.GetAxis(shape, axis);

            //prepare ret
            var ret = new NDArray(typeCode ?? (arr.GetTypeCode.GetAccumulatingType()), axisedShape, false);
            var iterAxis = new NDCoordinatesAxisIncrementor(ref shape, axis);
            var iterRet = new NDCoordinatesIncrementor(ref axisedShape);
            var iterIndex = iterRet.Index;
            var slices = iterAxis.Slices;

#if _REGEN
            #region Compute
            switch (arr.GetTypeCode)
		    {
			    %foreach supported_numericals,supported_numericals_lowercase%
			    case NPTypeCode.#1: 
                {
                    switch (ret.GetTypeCode)
		            {
			            %foreach supported_numericals,supported_numericals_lowercase%
			            case NPTypeCode.#101: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<#2>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                |#102 sum = default;
                                while (hasNext())
                                    sum += (#102) moveNext();

                                ret.Set#101(sum, iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            %
			            default:
				            throw new NotSupportedException();
		            }
                    break;
                }
			    %
			    default:
				    throw new NotSupportedException();
		    }
            #endregion
#else

            #region Compute
            switch (arr.GetTypeCode)
		    {
			    case NPTypeCode.Byte: 
                {
                    switch (ret.GetTypeCode)
		            {
			            case NPTypeCode.Byte: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<byte>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                byte sum = default;
                                while (hasNext())
                                    sum += (byte) moveNext();

                                ret.SetByte(Convert.ToByte(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Int16: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<byte>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                short sum = default;
                                while (hasNext())
                                    sum += (short) moveNext();

                                ret.SetInt16(Convert.ToInt16(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.UInt16: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<byte>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                ushort sum = default;
                                while (hasNext())
                                    sum += (ushort) moveNext();

                                ret.SetUInt16(Convert.ToUInt16(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Int32: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<byte>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                int sum = default;
                                while (hasNext())
                                    sum += (int) moveNext();

                                ret.SetInt32(Convert.ToInt32(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.UInt32: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<byte>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                uint sum = default;
                                while (hasNext())
                                    sum += (uint) moveNext();

                                ret.SetUInt32(Convert.ToUInt32(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Int64: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<byte>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                long sum = default;
                                while (hasNext())
                                    sum += (long) moveNext();

                                ret.SetInt64(Convert.ToInt64(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.UInt64: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<byte>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                ulong sum = default;
                                while (hasNext())
                                    sum += (ulong) moveNext();

                                ret.SetUInt64(Convert.ToUInt64(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Char: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<byte>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                char sum = default;
                                while (hasNext())
                                    sum += (char) moveNext();

                                ret.SetChar(Convert.ToChar(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Double: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<byte>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                double sum = default;
                                while (hasNext())
                                    sum += (double) moveNext();

                                ret.SetDouble(Convert.ToDouble(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Single: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<byte>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                float sum = default;
                                while (hasNext())
                                    sum += (float) moveNext();

                                ret.SetSingle(Convert.ToSingle(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Decimal: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<byte>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                decimal sum = default;
                                while (hasNext())
                                    sum += (decimal) moveNext();

                                ret.SetDecimal(Convert.ToDecimal(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            default:
				            throw new NotSupportedException();
		            }
                    break;
                }
			    case NPTypeCode.Int16: 
                {
                    switch (ret.GetTypeCode)
		            {
			            case NPTypeCode.Byte: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<short>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                byte sum = default;
                                while (hasNext())
                                    sum += (byte) moveNext();

                                ret.SetByte(Convert.ToByte(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Int16: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<short>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                short sum = default;
                                while (hasNext())
                                    sum += (short) moveNext();

                                ret.SetInt16(Convert.ToInt16(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.UInt16: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<short>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                ushort sum = default;
                                while (hasNext())
                                    sum += (ushort) moveNext();

                                ret.SetUInt16(Convert.ToUInt16(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Int32: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<short>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                int sum = default;
                                while (hasNext())
                                    sum += (int) moveNext();

                                ret.SetInt32(Convert.ToInt32(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.UInt32: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<short>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                uint sum = default;
                                while (hasNext())
                                    sum += (uint) moveNext();

                                ret.SetUInt32(Convert.ToUInt32(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Int64: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<short>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                long sum = default;
                                while (hasNext())
                                    sum += (long) moveNext();

                                ret.SetInt64(Convert.ToInt64(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.UInt64: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<short>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                ulong sum = default;
                                while (hasNext())
                                    sum += (ulong) moveNext();

                                ret.SetUInt64(Convert.ToUInt64(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Char: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<short>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                char sum = default;
                                while (hasNext())
                                    sum += (char) moveNext();

                                ret.SetChar(Convert.ToChar(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Double: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<short>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                double sum = default;
                                while (hasNext())
                                    sum += (double) moveNext();

                                ret.SetDouble(Convert.ToDouble(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Single: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<short>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                float sum = default;
                                while (hasNext())
                                    sum += (float) moveNext();

                                ret.SetSingle(Convert.ToSingle(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Decimal: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<short>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                decimal sum = default;
                                while (hasNext())
                                    sum += (decimal) moveNext();

                                ret.SetDecimal(Convert.ToDecimal(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            default:
				            throw new NotSupportedException();
		            }
                    break;
                }
			    case NPTypeCode.UInt16: 
                {
                    switch (ret.GetTypeCode)
		            {
			            case NPTypeCode.Byte: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<ushort>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                byte sum = default;
                                while (hasNext())
                                    sum += (byte) moveNext();

                                ret.SetByte(Convert.ToByte(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Int16: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<ushort>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                short sum = default;
                                while (hasNext())
                                    sum += (short) moveNext();

                                ret.SetInt16(Convert.ToInt16(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.UInt16: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<ushort>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                ushort sum = default;
                                while (hasNext())
                                    sum += (ushort) moveNext();

                                ret.SetUInt16(Convert.ToUInt16(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Int32: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<ushort>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                int sum = default;
                                while (hasNext())
                                    sum += (int) moveNext();

                                ret.SetInt32(Convert.ToInt32(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.UInt32: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<ushort>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                uint sum = default;
                                while (hasNext())
                                    sum += (uint) moveNext();

                                ret.SetUInt32(Convert.ToUInt32(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Int64: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<ushort>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                long sum = default;
                                while (hasNext())
                                    sum += (long) moveNext();

                                ret.SetInt64(Convert.ToInt64(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.UInt64: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<ushort>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                ulong sum = default;
                                while (hasNext())
                                    sum += (ulong) moveNext();

                                ret.SetUInt64(Convert.ToUInt64(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Char: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<ushort>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                char sum = default;
                                while (hasNext())
                                    sum += (char) moveNext();

                                ret.SetChar(Convert.ToChar(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Double: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<ushort>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                double sum = default;
                                while (hasNext())
                                    sum += (double) moveNext();

                                ret.SetDouble(Convert.ToDouble(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Single: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<ushort>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                float sum = default;
                                while (hasNext())
                                    sum += (float) moveNext();

                                ret.SetSingle(Convert.ToSingle(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Decimal: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<ushort>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                decimal sum = default;
                                while (hasNext())
                                    sum += (decimal) moveNext();

                                ret.SetDecimal(Convert.ToDecimal(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            default:
				            throw new NotSupportedException();
		            }
                    break;
                }
			    case NPTypeCode.Int32: 
                {
                    switch (ret.GetTypeCode)
		            {
			            case NPTypeCode.Byte: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<int>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                byte sum = default;
                                while (hasNext())
                                    sum += (byte) moveNext();

                                ret.SetByte(Convert.ToByte(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Int16: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<int>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                short sum = default;
                                while (hasNext())
                                    sum += (short) moveNext();

                                ret.SetInt16(Convert.ToInt16(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.UInt16: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<int>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                ushort sum = default;
                                while (hasNext())
                                    sum += (ushort) moveNext();

                                ret.SetUInt16(Convert.ToUInt16(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Int32: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<int>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                int sum = default;
                                while (hasNext())
                                    sum += (int) moveNext();

                                ret.SetInt32(Convert.ToInt32(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.UInt32: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<int>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                uint sum = default;
                                while (hasNext())
                                    sum += (uint) moveNext();

                                ret.SetUInt32(Convert.ToUInt32(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Int64: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<int>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                long sum = default;
                                while (hasNext())
                                    sum += (long) moveNext();

                                ret.SetInt64(Convert.ToInt64(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.UInt64: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<int>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                ulong sum = default;
                                while (hasNext())
                                    sum += (ulong) moveNext();

                                ret.SetUInt64(Convert.ToUInt64(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Char: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<int>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                char sum = default;
                                while (hasNext())
                                    sum += (char) moveNext();

                                ret.SetChar(Convert.ToChar(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Double: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<int>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                double sum = default;
                                while (hasNext())
                                    sum += (double) moveNext();

                                ret.SetDouble(Convert.ToDouble(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Single: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<int>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                float sum = default;
                                while (hasNext())
                                    sum += (float) moveNext();

                                ret.SetSingle(Convert.ToSingle(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Decimal: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<int>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                decimal sum = default;
                                while (hasNext())
                                    sum += (decimal) moveNext();

                                ret.SetDecimal(Convert.ToDecimal(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            default:
				            throw new NotSupportedException();
		            }
                    break;
                }
			    case NPTypeCode.UInt32: 
                {
                    switch (ret.GetTypeCode)
		            {
			            case NPTypeCode.Byte: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<uint>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                byte sum = default;
                                while (hasNext())
                                    sum += (byte) moveNext();

                                ret.SetByte(Convert.ToByte(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Int16: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<uint>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                short sum = default;
                                while (hasNext())
                                    sum += (short) moveNext();

                                ret.SetInt16(Convert.ToInt16(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.UInt16: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<uint>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                ushort sum = default;
                                while (hasNext())
                                    sum += (ushort) moveNext();

                                ret.SetUInt16(Convert.ToUInt16(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Int32: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<uint>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                int sum = default;
                                while (hasNext())
                                    sum += (int) moveNext();

                                ret.SetInt32(Convert.ToInt32(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.UInt32: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<uint>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                uint sum = default;
                                while (hasNext())
                                    sum += (uint) moveNext();

                                ret.SetUInt32(Convert.ToUInt32(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Int64: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<uint>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                long sum = default;
                                while (hasNext())
                                    sum += (long) moveNext();

                                ret.SetInt64(Convert.ToInt64(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.UInt64: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<uint>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                ulong sum = default;
                                while (hasNext())
                                    sum += (ulong) moveNext();

                                ret.SetUInt64(Convert.ToUInt64(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Char: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<uint>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                char sum = default;
                                while (hasNext())
                                    sum += (char) moveNext();

                                ret.SetChar(Convert.ToChar(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Double: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<uint>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                double sum = default;
                                while (hasNext())
                                    sum += (double) moveNext();

                                ret.SetDouble(Convert.ToDouble(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Single: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<uint>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                float sum = default;
                                while (hasNext())
                                    sum += (float) moveNext();

                                ret.SetSingle(Convert.ToSingle(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Decimal: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<uint>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                decimal sum = default;
                                while (hasNext())
                                    sum += (decimal) moveNext();

                                ret.SetDecimal(Convert.ToDecimal(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            default:
				            throw new NotSupportedException();
		            }
                    break;
                }
			    case NPTypeCode.Int64: 
                {
                    switch (ret.GetTypeCode)
		            {
			            case NPTypeCode.Byte: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<long>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                byte sum = default;
                                while (hasNext())
                                    sum += (byte) moveNext();

                                ret.SetByte(Convert.ToByte(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Int16: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<long>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                short sum = default;
                                while (hasNext())
                                    sum += (short) moveNext();

                                ret.SetInt16(Convert.ToInt16(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.UInt16: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<long>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                ushort sum = default;
                                while (hasNext())
                                    sum += (ushort) moveNext();

                                ret.SetUInt16(Convert.ToUInt16(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Int32: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<long>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                int sum = default;
                                while (hasNext())
                                    sum += (int) moveNext();

                                ret.SetInt32(Convert.ToInt32(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.UInt32: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<long>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                uint sum = default;
                                while (hasNext())
                                    sum += (uint) moveNext();

                                ret.SetUInt32(Convert.ToUInt32(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Int64: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<long>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                long sum = default;
                                while (hasNext())
                                    sum += (long) moveNext();

                                ret.SetInt64(Convert.ToInt64(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.UInt64: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<long>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                ulong sum = default;
                                while (hasNext())
                                    sum += (ulong) moveNext();

                                ret.SetUInt64(Convert.ToUInt64(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Char: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<long>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                char sum = default;
                                while (hasNext())
                                    sum += (char) moveNext();

                                ret.SetChar(Convert.ToChar(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Double: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<long>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                double sum = default;
                                while (hasNext())
                                    sum += (double) moveNext();

                                ret.SetDouble(Convert.ToDouble(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Single: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<long>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                float sum = default;
                                while (hasNext())
                                    sum += (float) moveNext();

                                ret.SetSingle(Convert.ToSingle(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Decimal: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<long>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                decimal sum = default;
                                while (hasNext())
                                    sum += (decimal) moveNext();

                                ret.SetDecimal(Convert.ToDecimal(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            default:
				            throw new NotSupportedException();
		            }
                    break;
                }
			    case NPTypeCode.UInt64: 
                {
                    switch (ret.GetTypeCode)
		            {
			            case NPTypeCode.Byte: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<ulong>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                byte sum = default;
                                while (hasNext())
                                    sum += (byte) moveNext();

                                ret.SetByte(Convert.ToByte(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Int16: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<ulong>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                short sum = default;
                                while (hasNext())
                                    sum += (short) moveNext();

                                ret.SetInt16(Convert.ToInt16(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.UInt16: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<ulong>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                ushort sum = default;
                                while (hasNext())
                                    sum += (ushort) moveNext();

                                ret.SetUInt16(Convert.ToUInt16(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Int32: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<ulong>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                int sum = default;
                                while (hasNext())
                                    sum += (int) moveNext();

                                ret.SetInt32(Convert.ToInt32(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.UInt32: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<ulong>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                uint sum = default;
                                while (hasNext())
                                    sum += (uint) moveNext();

                                ret.SetUInt32(Convert.ToUInt32(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Int64: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<ulong>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                long sum = default;
                                while (hasNext())
                                    sum += (long) moveNext();

                                ret.SetInt64(Convert.ToInt64(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.UInt64: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<ulong>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                ulong sum = default;
                                while (hasNext())
                                    sum += (ulong) moveNext();

                                ret.SetUInt64(Convert.ToUInt64(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Char: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<ulong>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                char sum = default;
                                while (hasNext())
                                    sum += (char) moveNext();

                                ret.SetChar(Convert.ToChar(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Double: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<ulong>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                double sum = default;
                                while (hasNext())
                                    sum += (double) moveNext();

                                ret.SetDouble(Convert.ToDouble(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Single: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<ulong>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                float sum = default;
                                while (hasNext())
                                    sum += (float) moveNext();

                                ret.SetSingle(Convert.ToSingle(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Decimal: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<ulong>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                decimal sum = default;
                                while (hasNext())
                                    sum += (decimal) moveNext();

                                ret.SetDecimal(Convert.ToDecimal(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            default:
				            throw new NotSupportedException();
		            }
                    break;
                }
			    case NPTypeCode.Char: 
                {
                    switch (ret.GetTypeCode)
		            {
			            case NPTypeCode.Byte: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<char>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                byte sum = default;
                                while (hasNext())
                                    sum += (byte) moveNext();

                                ret.SetByte(Convert.ToByte(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Int16: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<char>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                short sum = default;
                                while (hasNext())
                                    sum += (short) moveNext();

                                ret.SetInt16(Convert.ToInt16(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.UInt16: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<char>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                ushort sum = default;
                                while (hasNext())
                                    sum += (ushort) moveNext();

                                ret.SetUInt16(Convert.ToUInt16(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Int32: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<char>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                int sum = default;
                                while (hasNext())
                                    sum += (int) moveNext();

                                ret.SetInt32(Convert.ToInt32(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.UInt32: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<char>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                uint sum = default;
                                while (hasNext())
                                    sum += (uint) moveNext();

                                ret.SetUInt32(Convert.ToUInt32(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Int64: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<char>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                long sum = default;
                                while (hasNext())
                                    sum += (long) moveNext();

                                ret.SetInt64(Convert.ToInt64(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.UInt64: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<char>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                ulong sum = default;
                                while (hasNext())
                                    sum += (ulong) moveNext();

                                ret.SetUInt64(Convert.ToUInt64(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Char: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<char>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                char sum = default;
                                while (hasNext())
                                    sum += (char) moveNext();

                                ret.SetChar(Convert.ToChar(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Double: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<char>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                double sum = default;
                                while (hasNext())
                                    sum += (double) moveNext();

                                ret.SetDouble(Convert.ToDouble(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Single: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<char>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                float sum = default;
                                while (hasNext())
                                    sum += (float) moveNext();

                                ret.SetSingle(Convert.ToSingle(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Decimal: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<char>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                decimal sum = default;
                                while (hasNext())
                                    sum += (decimal) moveNext();

                                ret.SetDecimal(Convert.ToDecimal(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            default:
				            throw new NotSupportedException();
		            }
                    break;
                }
			    case NPTypeCode.Double: 
                {
                    switch (ret.GetTypeCode)
		            {
			            case NPTypeCode.Byte: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<double>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                byte sum = default;
                                while (hasNext())
                                    sum += (byte) moveNext();

                                ret.SetByte(Convert.ToByte(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Int16: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<double>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                short sum = default;
                                while (hasNext())
                                    sum += (short) moveNext();

                                ret.SetInt16(Convert.ToInt16(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.UInt16: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<double>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                ushort sum = default;
                                while (hasNext())
                                    sum += (ushort) moveNext();

                                ret.SetUInt16(Convert.ToUInt16(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Int32: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<double>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                int sum = default;
                                while (hasNext())
                                    sum += (int) moveNext();

                                ret.SetInt32(Convert.ToInt32(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.UInt32: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<double>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                uint sum = default;
                                while (hasNext())
                                    sum += (uint) moveNext();

                                ret.SetUInt32(Convert.ToUInt32(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Int64: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<double>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                long sum = default;
                                while (hasNext())
                                    sum += (long) moveNext();

                                ret.SetInt64(Convert.ToInt64(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.UInt64: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<double>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                ulong sum = default;
                                while (hasNext())
                                    sum += (ulong) moveNext();

                                ret.SetUInt64(Convert.ToUInt64(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Char: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<double>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                char sum = default;
                                while (hasNext())
                                    sum += (char) moveNext();

                                ret.SetChar(Convert.ToChar(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Double: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<double>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                double sum = default;
                                while (hasNext())
                                    sum += (double) moveNext();

                                ret.SetDouble(Convert.ToDouble(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Single: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<double>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                float sum = default;
                                while (hasNext())
                                    sum += (float) moveNext();

                                ret.SetSingle(Convert.ToSingle(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Decimal: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<double>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                decimal sum = default;
                                while (hasNext())
                                    sum += (decimal) moveNext();

                                ret.SetDecimal(Convert.ToDecimal(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            default:
				            throw new NotSupportedException();
		            }
                    break;
                }
			    case NPTypeCode.Single: 
                {
                    switch (ret.GetTypeCode)
		            {
			            case NPTypeCode.Byte: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<float>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                byte sum = default;
                                while (hasNext())
                                    sum += (byte) moveNext();

                                ret.SetByte(Convert.ToByte(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Int16: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<float>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                short sum = default;
                                while (hasNext())
                                    sum += (short) moveNext();

                                ret.SetInt16(Convert.ToInt16(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.UInt16: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<float>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                ushort sum = default;
                                while (hasNext())
                                    sum += (ushort) moveNext();

                                ret.SetUInt16(Convert.ToUInt16(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Int32: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<float>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                int sum = default;
                                while (hasNext())
                                    sum += (int) moveNext();

                                ret.SetInt32(Convert.ToInt32(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.UInt32: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<float>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                uint sum = default;
                                while (hasNext())
                                    sum += (uint) moveNext();

                                ret.SetUInt32(Convert.ToUInt32(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Int64: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<float>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                long sum = default;
                                while (hasNext())
                                    sum += (long) moveNext();

                                ret.SetInt64(Convert.ToInt64(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.UInt64: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<float>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                ulong sum = default;
                                while (hasNext())
                                    sum += (ulong) moveNext();

                                ret.SetUInt64(Convert.ToUInt64(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Char: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<float>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                char sum = default;
                                while (hasNext())
                                    sum += (char) moveNext();

                                ret.SetChar(Convert.ToChar(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Double: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<float>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                double sum = default;
                                while (hasNext())
                                    sum += (double) moveNext();

                                ret.SetDouble(Convert.ToDouble(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Single: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<float>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                float sum = default;
                                while (hasNext())
                                    sum += (float) moveNext();

                                ret.SetSingle(Convert.ToSingle(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Decimal: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<float>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                decimal sum = default;
                                while (hasNext())
                                    sum += (decimal) moveNext();

                                ret.SetDecimal(Convert.ToDecimal(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            default:
				            throw new NotSupportedException();
		            }
                    break;
                }
			    case NPTypeCode.Decimal: 
                {
                    switch (ret.GetTypeCode)
		            {
			            case NPTypeCode.Byte: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<decimal>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                byte sum = default;
                                while (hasNext())
                                    sum += (byte) moveNext();

                                ret.SetByte(Convert.ToByte(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Int16: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<decimal>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                short sum = default;
                                while (hasNext())
                                    sum += (short) moveNext();

                                ret.SetInt16(Convert.ToInt16(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.UInt16: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<decimal>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                ushort sum = default;
                                while (hasNext())
                                    sum += (ushort) moveNext();

                                ret.SetUInt16(Convert.ToUInt16(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Int32: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<decimal>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                int sum = default;
                                while (hasNext())
                                    sum += (int) moveNext();

                                ret.SetInt32(Convert.ToInt32(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.UInt32: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<decimal>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                uint sum = default;
                                while (hasNext())
                                    sum += (uint) moveNext();

                                ret.SetUInt32(Convert.ToUInt32(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Int64: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<decimal>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                long sum = default;
                                while (hasNext())
                                    sum += (long) moveNext();

                                ret.SetInt64(Convert.ToInt64(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.UInt64: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<decimal>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                ulong sum = default;
                                while (hasNext())
                                    sum += (ulong) moveNext();

                                ret.SetUInt64(Convert.ToUInt64(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Char: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<decimal>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                char sum = default;
                                while (hasNext())
                                    sum += (char) moveNext();

                                ret.SetChar(Convert.ToChar(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Double: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<decimal>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                double sum = default;
                                while (hasNext())
                                    sum += (double) moveNext();

                                ret.SetDouble(Convert.ToDouble(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Single: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<decimal>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                float sum = default;
                                while (hasNext())
                                    sum += (float) moveNext();

                                ret.SetSingle(Convert.ToSingle(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            case NPTypeCode.Decimal: 
                        {
                            do
                            {
                                var iter = arr[slices].AsIterator<decimal>();
                                var moveNext = iter.MoveNext;
                                var hasNext = iter.HasNext;
                                decimal sum = default;
                                while (hasNext())
                                    sum += (decimal) moveNext();

                                ret.SetDecimal(Convert.ToDecimal(sum), iterIndex);
                            } while (iterAxis.Next() != null && iterRet.Next() != null);
                            break;
                        }
			            default:
				            throw new NotSupportedException();
		            }
                    break;
                }
			    default:
				    throw new NotSupportedException();
		    }
            #endregion
#endif

            if (keepdims)
                ret.Storage.ExpandDimension(axis);

            return ret;
        }

        public T SumElementwise<T>(NDArray arr, NPTypeCode? typeCode) where T : unmanaged
        {
            return (T)Convert.ChangeType(sum_elementwise(arr, typeCode), typeof(T));
        }

        protected object sum_elementwise(NDArray arr, NPTypeCode? typeCode)
        {
            if (arr.Shape.IsScalar || (arr.Shape.NDim == 1 && arr.Shape.size == 1))
                return typeCode.HasValue ? Cast(arr, typeCode.Value, true) : arr.Clone();

            var retType = typeCode ?? (arr.GetTypeCode.GetAccumulatingType());
#if _REGEN
            #region Compute
            switch (arr.GetTypeCode)
		    {
			    %foreach supported_numericals,supported_numericals_lowercase%
			    case NPTypeCode.#1: 
                {
                    switch (retType)
		            {
			            %foreach supported_numericals,supported_numericals_lowercase%
			            case NPTypeCode.#101: 
                        {
                            var iter = arr.AsIterator<#2>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            |#102 sum = default;
                            while (hasNext())
                                sum += (#102) moveNext();

                            return sum;
                        }
			            %
			            default:
				            throw new NotSupportedException();
		            }
                    break;
                }
			    %
			    default:
				    throw new NotSupportedException();
		    }
            #endregion
#else

            #region Compute
            switch (arr.GetTypeCode)
		    {
			    case NPTypeCode.Byte: 
                {
                    switch (retType)
		            {
			            case NPTypeCode.Byte: 
                        {
                            var iter = arr.AsIterator<byte>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt32 sum = 0;
                            while (hasNext())
                                sum += (UInt32) moveNext();

                            return Convert.ToByte(sum);
                        }
			            case NPTypeCode.Int16: 
                        {
                            var iter = arr.AsIterator<byte>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Int32 sum = 0;
                            while (hasNext())
                                sum += (Int32) moveNext();

                            return Convert.ToInt16(sum);
                        }
			            case NPTypeCode.UInt16: 
                        {
                            var iter = arr.AsIterator<byte>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt32 sum = 0;
                            while (hasNext())
                                sum += (UInt32) moveNext();

                            return Convert.ToUInt16(sum);
                        }
			            case NPTypeCode.Int32: 
                        {
                            var iter = arr.AsIterator<byte>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Int32 sum = 0;
                            while (hasNext())
                                sum += (Int32) moveNext();

                            return Convert.ToInt32(sum);
                        }
			            case NPTypeCode.UInt32: 
                        {
                            var iter = arr.AsIterator<byte>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt32 sum = 0u;
                            while (hasNext())
                                sum += (UInt32) moveNext();

                            return Convert.ToUInt32(sum);
                        }
			            case NPTypeCode.Int64: 
                        {
                            var iter = arr.AsIterator<byte>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Int64 sum = 0L;
                            while (hasNext())
                                sum += (Int64) moveNext();

                            return Convert.ToInt64(sum);
                        }
			            case NPTypeCode.UInt64: 
                        {
                            var iter = arr.AsIterator<byte>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt64 sum = 0UL;
                            while (hasNext())
                                sum += (UInt64) moveNext();

                            return Convert.ToUInt64(sum);
                        }
			            case NPTypeCode.Char: 
                        {
                            var iter = arr.AsIterator<byte>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt32 sum = '\0';
                            while (hasNext())
                                sum += (UInt32) moveNext();

                            return Convert.ToChar(sum);
                        }
			            case NPTypeCode.Double: 
                        {
                            var iter = arr.AsIterator<byte>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Double sum = 0d;
                            while (hasNext())
                                sum += (Double) moveNext();

                            return Convert.ToDouble(sum);
                        }
			            case NPTypeCode.Single: 
                        {
                            var iter = arr.AsIterator<byte>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Single sum = 0f;
                            while (hasNext())
                                sum += (Single) moveNext();

                            return Convert.ToSingle(sum);
                        }
			            case NPTypeCode.Decimal: 
                        {
                            var iter = arr.AsIterator<byte>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Decimal sum = 0m;
                            while (hasNext())
                                sum += (Decimal) moveNext();

                            return Convert.ToDecimal(sum);
                        }
			            default:
				            throw new NotSupportedException();
		            }
                    break;
                }
			    case NPTypeCode.Int16: 
                {
                    switch (retType)
		            {
			            case NPTypeCode.Byte: 
                        {
                            var iter = arr.AsIterator<short>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt32 sum = 0;
                            while (hasNext())
                                sum += (UInt32) moveNext();

                            return Convert.ToByte(sum);
                        }
			            case NPTypeCode.Int16: 
                        {
                            var iter = arr.AsIterator<short>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Int32 sum = 0;
                            while (hasNext())
                                sum += (Int32) moveNext();

                            return Convert.ToInt16(sum);
                        }
			            case NPTypeCode.UInt16: 
                        {
                            var iter = arr.AsIterator<short>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt32 sum = 0;
                            while (hasNext())
                                sum += (UInt32) moveNext();

                            return Convert.ToUInt16(sum);
                        }
			            case NPTypeCode.Int32: 
                        {
                            var iter = arr.AsIterator<short>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Int32 sum = 0;
                            while (hasNext())
                                sum += (Int32) moveNext();

                            return Convert.ToInt32(sum);
                        }
			            case NPTypeCode.UInt32: 
                        {
                            var iter = arr.AsIterator<short>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt32 sum = 0u;
                            while (hasNext())
                                sum += (UInt32) moveNext();

                            return Convert.ToUInt32(sum);
                        }
			            case NPTypeCode.Int64: 
                        {
                            var iter = arr.AsIterator<short>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Int64 sum = 0L;
                            while (hasNext())
                                sum += (Int64) moveNext();

                            return Convert.ToInt64(sum);
                        }
			            case NPTypeCode.UInt64: 
                        {
                            var iter = arr.AsIterator<short>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt64 sum = 0UL;
                            while (hasNext())
                                sum += (UInt64) moveNext();

                            return Convert.ToUInt64(sum);
                        }
			            case NPTypeCode.Char: 
                        {
                            var iter = arr.AsIterator<short>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt32 sum = '\0';
                            while (hasNext())
                                sum += (UInt32) moveNext();

                            return Convert.ToChar(sum);
                        }
			            case NPTypeCode.Double: 
                        {
                            var iter = arr.AsIterator<short>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Double sum = 0d;
                            while (hasNext())
                                sum += (Double) moveNext();

                            return Convert.ToDouble(sum);
                        }
			            case NPTypeCode.Single: 
                        {
                            var iter = arr.AsIterator<short>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Single sum = 0f;
                            while (hasNext())
                                sum += (Single) moveNext();

                            return Convert.ToSingle(sum);
                        }
			            case NPTypeCode.Decimal: 
                        {
                            var iter = arr.AsIterator<short>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Decimal sum = 0m;
                            while (hasNext())
                                sum += (Decimal) moveNext();

                            return Convert.ToDecimal(sum);
                        }
			            default:
				            throw new NotSupportedException();
		            }
                    break;
                }
			    case NPTypeCode.UInt16: 
                {
                    switch (retType)
		            {
			            case NPTypeCode.Byte: 
                        {
                            var iter = arr.AsIterator<ushort>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt32 sum = 0;
                            while (hasNext())
                                sum += (UInt32) moveNext();

                            return Convert.ToByte(sum);
                        }
			            case NPTypeCode.Int16: 
                        {
                            var iter = arr.AsIterator<ushort>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Int32 sum = 0;
                            while (hasNext())
                                sum += (Int32) moveNext();

                            return Convert.ToInt16(sum);
                        }
			            case NPTypeCode.UInt16: 
                        {
                            var iter = arr.AsIterator<ushort>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt32 sum = 0;
                            while (hasNext())
                                sum += (UInt32) moveNext();

                            return Convert.ToUInt16(sum);
                        }
			            case NPTypeCode.Int32: 
                        {
                            var iter = arr.AsIterator<ushort>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Int32 sum = 0;
                            while (hasNext())
                                sum += (Int32) moveNext();

                            return Convert.ToInt32(sum);
                        }
			            case NPTypeCode.UInt32: 
                        {
                            var iter = arr.AsIterator<ushort>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt32 sum = 0u;
                            while (hasNext())
                                sum += (UInt32) moveNext();

                            return Convert.ToUInt32(sum);
                        }
			            case NPTypeCode.Int64: 
                        {
                            var iter = arr.AsIterator<ushort>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Int64 sum = 0L;
                            while (hasNext())
                                sum += (Int64) moveNext();

                            return Convert.ToInt64(sum);
                        }
			            case NPTypeCode.UInt64: 
                        {
                            var iter = arr.AsIterator<ushort>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt64 sum = 0UL;
                            while (hasNext())
                                sum += (UInt64) moveNext();

                            return Convert.ToUInt64(sum);
                        }
			            case NPTypeCode.Char: 
                        {
                            var iter = arr.AsIterator<ushort>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt32 sum = '\0';
                            while (hasNext())
                                sum += (UInt32) moveNext();

                            return Convert.ToChar(sum);
                        }
			            case NPTypeCode.Double: 
                        {
                            var iter = arr.AsIterator<ushort>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Double sum = 0d;
                            while (hasNext())
                                sum += (Double) moveNext();

                            return Convert.ToDouble(sum);
                        }
			            case NPTypeCode.Single: 
                        {
                            var iter = arr.AsIterator<ushort>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Single sum = 0f;
                            while (hasNext())
                                sum += (Single) moveNext();

                            return Convert.ToSingle(sum);
                        }
			            case NPTypeCode.Decimal: 
                        {
                            var iter = arr.AsIterator<ushort>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Decimal sum = 0m;
                            while (hasNext())
                                sum += (Decimal) moveNext();

                            return Convert.ToDecimal(sum);
                        }
			            default:
				            throw new NotSupportedException();
		            }
                    break;
                }
			    case NPTypeCode.Int32: 
                {
                    switch (retType)
		            {
			            case NPTypeCode.Byte: 
                        {
                            var iter = arr.AsIterator<int>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt32 sum = 0;
                            while (hasNext())
                                sum += (UInt32) moveNext();

                            return Convert.ToByte(sum);
                        }
			            case NPTypeCode.Int16: 
                        {
                            var iter = arr.AsIterator<int>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Int32 sum = 0;
                            while (hasNext())
                                sum += (Int32) moveNext();

                            return Convert.ToInt16(sum);
                        }
			            case NPTypeCode.UInt16: 
                        {
                            var iter = arr.AsIterator<int>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt32 sum = 0;
                            while (hasNext())
                                sum += (UInt32) moveNext();

                            return Convert.ToUInt16(sum);
                        }
			            case NPTypeCode.Int32: 
                        {
                            var iter = arr.AsIterator<int>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Int32 sum = 0;
                            while (hasNext())
                                sum += (Int32) moveNext();

                            return Convert.ToInt32(sum);
                        }
			            case NPTypeCode.UInt32: 
                        {
                            var iter = arr.AsIterator<int>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt32 sum = 0u;
                            while (hasNext())
                                sum += (UInt32) moveNext();

                            return Convert.ToUInt32(sum);
                        }
			            case NPTypeCode.Int64: 
                        {
                            var iter = arr.AsIterator<int>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Int64 sum = 0L;
                            while (hasNext())
                                sum += (Int64) moveNext();

                            return Convert.ToInt64(sum);
                        }
			            case NPTypeCode.UInt64: 
                        {
                            var iter = arr.AsIterator<int>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt64 sum = 0UL;
                            while (hasNext())
                                sum += (UInt64) moveNext();

                            return Convert.ToUInt64(sum);
                        }
			            case NPTypeCode.Char: 
                        {
                            var iter = arr.AsIterator<int>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt32 sum = '\0';
                            while (hasNext())
                                sum += (UInt32) moveNext();

                            return Convert.ToChar(sum);
                        }
			            case NPTypeCode.Double: 
                        {
                            var iter = arr.AsIterator<int>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Double sum = 0d;
                            while (hasNext())
                                sum += (Double) moveNext();

                            return Convert.ToDouble(sum);
                        }
			            case NPTypeCode.Single: 
                        {
                            var iter = arr.AsIterator<int>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Single sum = 0f;
                            while (hasNext())
                                sum += (Single) moveNext();

                            return Convert.ToSingle(sum);
                        }
			            case NPTypeCode.Decimal: 
                        {
                            var iter = arr.AsIterator<int>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Decimal sum = 0m;
                            while (hasNext())
                                sum += (Decimal) moveNext();

                            return Convert.ToDecimal(sum);
                        }
			            default:
				            throw new NotSupportedException();
		            }
                    break;
                }
			    case NPTypeCode.UInt32: 
                {
                    switch (retType)
		            {
			            case NPTypeCode.Byte: 
                        {
                            var iter = arr.AsIterator<uint>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt32 sum = 0;
                            while (hasNext())
                                sum += (UInt32) moveNext();

                            return Convert.ToByte(sum);
                        }
			            case NPTypeCode.Int16: 
                        {
                            var iter = arr.AsIterator<uint>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Int32 sum = 0;
                            while (hasNext())
                                sum += (Int32) moveNext();

                            return Convert.ToInt16(sum);
                        }
			            case NPTypeCode.UInt16: 
                        {
                            var iter = arr.AsIterator<uint>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt32 sum = 0;
                            while (hasNext())
                                sum += (UInt32) moveNext();

                            return Convert.ToUInt16(sum);
                        }
			            case NPTypeCode.Int32: 
                        {
                            var iter = arr.AsIterator<uint>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Int32 sum = 0;
                            while (hasNext())
                                sum += (Int32) moveNext();

                            return Convert.ToInt32(sum);
                        }
			            case NPTypeCode.UInt32: 
                        {
                            var iter = arr.AsIterator<uint>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt32 sum = 0u;
                            while (hasNext())
                                sum += (UInt32) moveNext();

                            return Convert.ToUInt32(sum);
                        }
			            case NPTypeCode.Int64: 
                        {
                            var iter = arr.AsIterator<uint>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Int64 sum = 0L;
                            while (hasNext())
                                sum += (Int64) moveNext();

                            return Convert.ToInt64(sum);
                        }
			            case NPTypeCode.UInt64: 
                        {
                            var iter = arr.AsIterator<uint>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt64 sum = 0UL;
                            while (hasNext())
                                sum += (UInt64) moveNext();

                            return Convert.ToUInt64(sum);
                        }
			            case NPTypeCode.Char: 
                        {
                            var iter = arr.AsIterator<uint>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt32 sum = '\0';
                            while (hasNext())
                                sum += (UInt32) moveNext();

                            return Convert.ToChar(sum);
                        }
			            case NPTypeCode.Double: 
                        {
                            var iter = arr.AsIterator<uint>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Double sum = 0d;
                            while (hasNext())
                                sum += (Double) moveNext();

                            return Convert.ToDouble(sum);
                        }
			            case NPTypeCode.Single: 
                        {
                            var iter = arr.AsIterator<uint>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Single sum = 0f;
                            while (hasNext())
                                sum += (Single) moveNext();

                            return Convert.ToSingle(sum);
                        }
			            case NPTypeCode.Decimal: 
                        {
                            var iter = arr.AsIterator<uint>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Decimal sum = 0m;
                            while (hasNext())
                                sum += (Decimal) moveNext();

                            return Convert.ToDecimal(sum);
                        }
			            default:
				            throw new NotSupportedException();
		            }
                    break;
                }
			    case NPTypeCode.Int64: 
                {
                    switch (retType)
		            {
			            case NPTypeCode.Byte: 
                        {
                            var iter = arr.AsIterator<long>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt32 sum = 0;
                            while (hasNext())
                                sum += (UInt32) moveNext();

                            return Convert.ToByte(sum);
                        }
			            case NPTypeCode.Int16: 
                        {
                            var iter = arr.AsIterator<long>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Int32 sum = 0;
                            while (hasNext())
                                sum += (Int32) moveNext();

                            return Convert.ToInt16(sum);
                        }
			            case NPTypeCode.UInt16: 
                        {
                            var iter = arr.AsIterator<long>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt32 sum = 0;
                            while (hasNext())
                                sum += (UInt32) moveNext();

                            return Convert.ToUInt16(sum);
                        }
			            case NPTypeCode.Int32: 
                        {
                            var iter = arr.AsIterator<long>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Int32 sum = 0;
                            while (hasNext())
                                sum += (Int32) moveNext();

                            return Convert.ToInt32(sum);
                        }
			            case NPTypeCode.UInt32: 
                        {
                            var iter = arr.AsIterator<long>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt32 sum = 0u;
                            while (hasNext())
                                sum += (UInt32) moveNext();

                            return Convert.ToUInt32(sum);
                        }
			            case NPTypeCode.Int64: 
                        {
                            var iter = arr.AsIterator<long>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Int64 sum = 0L;
                            while (hasNext())
                                sum += (Int64) moveNext();

                            return Convert.ToInt64(sum);
                        }
			            case NPTypeCode.UInt64: 
                        {
                            var iter = arr.AsIterator<long>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt64 sum = 0UL;
                            while (hasNext())
                                sum += (UInt64) moveNext();

                            return Convert.ToUInt64(sum);
                        }
			            case NPTypeCode.Char: 
                        {
                            var iter = arr.AsIterator<long>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt32 sum = '\0';
                            while (hasNext())
                                sum += (UInt32) moveNext();

                            return Convert.ToChar(sum);
                        }
			            case NPTypeCode.Double: 
                        {
                            var iter = arr.AsIterator<long>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Double sum = 0d;
                            while (hasNext())
                                sum += (Double) moveNext();

                            return Convert.ToDouble(sum);
                        }
			            case NPTypeCode.Single: 
                        {
                            var iter = arr.AsIterator<long>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Single sum = 0f;
                            while (hasNext())
                                sum += (Single) moveNext();

                            return Convert.ToSingle(sum);
                        }
			            case NPTypeCode.Decimal: 
                        {
                            var iter = arr.AsIterator<long>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Decimal sum = 0m;
                            while (hasNext())
                                sum += (Decimal) moveNext();

                            return Convert.ToDecimal(sum);
                        }
			            default:
				            throw new NotSupportedException();
		            }
                    break;
                }
			    case NPTypeCode.UInt64: 
                {
                    switch (retType)
		            {
			            case NPTypeCode.Byte: 
                        {
                            var iter = arr.AsIterator<ulong>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt32 sum = 0;
                            while (hasNext())
                                sum += (UInt32) moveNext();

                            return Convert.ToByte(sum);
                        }
			            case NPTypeCode.Int16: 
                        {
                            var iter = arr.AsIterator<ulong>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Int32 sum = 0;
                            while (hasNext())
                                sum += (Int32) moveNext();

                            return Convert.ToInt16(sum);
                        }
			            case NPTypeCode.UInt16: 
                        {
                            var iter = arr.AsIterator<ulong>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt32 sum = 0;
                            while (hasNext())
                                sum += (UInt32) moveNext();

                            return Convert.ToUInt16(sum);
                        }
			            case NPTypeCode.Int32: 
                        {
                            var iter = arr.AsIterator<ulong>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Int32 sum = 0;
                            while (hasNext())
                                sum += (Int32) moveNext();

                            return Convert.ToInt32(sum);
                        }
			            case NPTypeCode.UInt32: 
                        {
                            var iter = arr.AsIterator<ulong>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt32 sum = 0u;
                            while (hasNext())
                                sum += (UInt32) moveNext();

                            return Convert.ToUInt32(sum);
                        }
			            case NPTypeCode.Int64: 
                        {
                            var iter = arr.AsIterator<ulong>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Int64 sum = 0L;
                            while (hasNext())
                                sum += (Int64) moveNext();

                            return Convert.ToInt64(sum);
                        }
			            case NPTypeCode.UInt64: 
                        {
                            var iter = arr.AsIterator<ulong>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt64 sum = 0UL;
                            while (hasNext())
                                sum += (UInt64) moveNext();

                            return Convert.ToUInt64(sum);
                        }
			            case NPTypeCode.Char: 
                        {
                            var iter = arr.AsIterator<ulong>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt32 sum = '\0';
                            while (hasNext())
                                sum += (UInt32) moveNext();

                            return Convert.ToChar(sum);
                        }
			            case NPTypeCode.Double: 
                        {
                            var iter = arr.AsIterator<ulong>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Double sum = 0d;
                            while (hasNext())
                                sum += (Double) moveNext();

                            return Convert.ToDouble(sum);
                        }
			            case NPTypeCode.Single: 
                        {
                            var iter = arr.AsIterator<ulong>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Single sum = 0f;
                            while (hasNext())
                                sum += (Single) moveNext();

                            return Convert.ToSingle(sum);
                        }
			            case NPTypeCode.Decimal: 
                        {
                            var iter = arr.AsIterator<ulong>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Decimal sum = 0m;
                            while (hasNext())
                                sum += (Decimal) moveNext();

                            return Convert.ToDecimal(sum);
                        }
			            default:
				            throw new NotSupportedException();
		            }
                    break;
                }
			    case NPTypeCode.Char: 
                {
                    switch (retType)
		            {
			            case NPTypeCode.Byte: 
                        {
                            var iter = arr.AsIterator<char>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt32 sum = 0;
                            while (hasNext())
                                sum += (UInt32) moveNext();

                            return Convert.ToByte(sum);
                        }
			            case NPTypeCode.Int16: 
                        {
                            var iter = arr.AsIterator<char>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Int32 sum = 0;
                            while (hasNext())
                                sum += (Int32) moveNext();

                            return Convert.ToInt16(sum);
                        }
			            case NPTypeCode.UInt16: 
                        {
                            var iter = arr.AsIterator<char>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt32 sum = 0;
                            while (hasNext())
                                sum += (UInt32) moveNext();

                            return Convert.ToUInt16(sum);
                        }
			            case NPTypeCode.Int32: 
                        {
                            var iter = arr.AsIterator<char>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Int32 sum = 0;
                            while (hasNext())
                                sum += (Int32) moveNext();

                            return Convert.ToInt32(sum);
                        }
			            case NPTypeCode.UInt32: 
                        {
                            var iter = arr.AsIterator<char>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt32 sum = 0u;
                            while (hasNext())
                                sum += (UInt32) moveNext();

                            return Convert.ToUInt32(sum);
                        }
			            case NPTypeCode.Int64: 
                        {
                            var iter = arr.AsIterator<char>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Int64 sum = 0L;
                            while (hasNext())
                                sum += (Int64) moveNext();

                            return Convert.ToInt64(sum);
                        }
			            case NPTypeCode.UInt64: 
                        {
                            var iter = arr.AsIterator<char>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt64 sum = 0UL;
                            while (hasNext())
                                sum += (UInt64) moveNext();

                            return Convert.ToUInt64(sum);
                        }
			            case NPTypeCode.Char: 
                        {
                            var iter = arr.AsIterator<char>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt32 sum = '\0';
                            while (hasNext())
                                sum += (UInt32) moveNext();

                            return Convert.ToChar(sum);
                        }
			            case NPTypeCode.Double: 
                        {
                            var iter = arr.AsIterator<char>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Double sum = 0d;
                            while (hasNext())
                                sum += (Double) moveNext();

                            return Convert.ToDouble(sum);
                        }
			            case NPTypeCode.Single: 
                        {
                            var iter = arr.AsIterator<char>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Single sum = 0f;
                            while (hasNext())
                                sum += (Single) moveNext();

                            return Convert.ToSingle(sum);
                        }
			            case NPTypeCode.Decimal: 
                        {
                            var iter = arr.AsIterator<char>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Decimal sum = 0m;
                            while (hasNext())
                                sum += (Decimal) moveNext();

                            return Convert.ToDecimal(sum);
                        }
			            default:
				            throw new NotSupportedException();
		            }
                    break;
                }
			    case NPTypeCode.Double: 
                {
                    switch (retType)
		            {
			            case NPTypeCode.Byte: 
                        {
                            var iter = arr.AsIterator<double>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt32 sum = 0;
                            while (hasNext())
                                sum += (UInt32) moveNext();

                            return Convert.ToByte(sum);
                        }
			            case NPTypeCode.Int16: 
                        {
                            var iter = arr.AsIterator<double>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Int32 sum = 0;
                            while (hasNext())
                                sum += (Int32) moveNext();

                            return Convert.ToInt16(sum);
                        }
			            case NPTypeCode.UInt16: 
                        {
                            var iter = arr.AsIterator<double>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt32 sum = 0;
                            while (hasNext())
                                sum += (UInt32) moveNext();

                            return Convert.ToUInt16(sum);
                        }
			            case NPTypeCode.Int32: 
                        {
                            var iter = arr.AsIterator<double>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Int32 sum = 0;
                            while (hasNext())
                                sum += (Int32) moveNext();

                            return Convert.ToInt32(sum);
                        }
			            case NPTypeCode.UInt32: 
                        {
                            var iter = arr.AsIterator<double>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt32 sum = 0u;
                            while (hasNext())
                                sum += (UInt32) moveNext();

                            return Convert.ToUInt32(sum);
                        }
			            case NPTypeCode.Int64: 
                        {
                            var iter = arr.AsIterator<double>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Int64 sum = 0L;
                            while (hasNext())
                                sum += (Int64) moveNext();

                            return Convert.ToInt64(sum);
                        }
			            case NPTypeCode.UInt64: 
                        {
                            var iter = arr.AsIterator<double>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt64 sum = 0UL;
                            while (hasNext())
                                sum += (UInt64) moveNext();

                            return Convert.ToUInt64(sum);
                        }
			            case NPTypeCode.Char: 
                        {
                            var iter = arr.AsIterator<double>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt32 sum = '\0';
                            while (hasNext())
                                sum += (UInt32) moveNext();

                            return Convert.ToChar(sum);
                        }
			            case NPTypeCode.Double: 
                        {
                            var iter = arr.AsIterator<double>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Double sum = 0d;
                            while (hasNext())
                                sum += (Double) moveNext();

                            return Convert.ToDouble(sum);
                        }
			            case NPTypeCode.Single: 
                        {
                            var iter = arr.AsIterator<double>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Single sum = 0f;
                            while (hasNext())
                                sum += (Single) moveNext();

                            return Convert.ToSingle(sum);
                        }
			            case NPTypeCode.Decimal: 
                        {
                            var iter = arr.AsIterator<double>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Decimal sum = 0m;
                            while (hasNext())
                                sum += (Decimal) moveNext();

                            return Convert.ToDecimal(sum);
                        }
			            default:
				            throw new NotSupportedException();
		            }
                    break;
                }
			    case NPTypeCode.Single: 
                {
                    switch (retType)
		            {
			            case NPTypeCode.Byte: 
                        {
                            var iter = arr.AsIterator<float>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt32 sum = 0;
                            while (hasNext())
                                sum += (UInt32) moveNext();

                            return Convert.ToByte(sum);
                        }
			            case NPTypeCode.Int16: 
                        {
                            var iter = arr.AsIterator<float>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Int32 sum = 0;
                            while (hasNext())
                                sum += (Int32) moveNext();

                            return Convert.ToInt16(sum);
                        }
			            case NPTypeCode.UInt16: 
                        {
                            var iter = arr.AsIterator<float>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt32 sum = 0;
                            while (hasNext())
                                sum += (UInt32) moveNext();

                            return Convert.ToUInt16(sum);
                        }
			            case NPTypeCode.Int32: 
                        {
                            var iter = arr.AsIterator<float>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Int32 sum = 0;
                            while (hasNext())
                                sum += (Int32) moveNext();

                            return Convert.ToInt32(sum);
                        }
			            case NPTypeCode.UInt32: 
                        {
                            var iter = arr.AsIterator<float>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt32 sum = 0u;
                            while (hasNext())
                                sum += (UInt32) moveNext();

                            return Convert.ToUInt32(sum);
                        }
			            case NPTypeCode.Int64: 
                        {
                            var iter = arr.AsIterator<float>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Int64 sum = 0L;
                            while (hasNext())
                                sum += (Int64) moveNext();

                            return Convert.ToInt64(sum);
                        }
			            case NPTypeCode.UInt64: 
                        {
                            var iter = arr.AsIterator<float>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt64 sum = 0UL;
                            while (hasNext())
                                sum += (UInt64) moveNext();

                            return Convert.ToUInt64(sum);
                        }
			            case NPTypeCode.Char: 
                        {
                            var iter = arr.AsIterator<float>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt32 sum = '\0';
                            while (hasNext())
                                sum += (UInt32) moveNext();

                            return Convert.ToChar(sum);
                        }
			            case NPTypeCode.Double: 
                        {
                            var iter = arr.AsIterator<float>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Double sum = 0d;
                            while (hasNext())
                                sum += (Double) moveNext();

                            return Convert.ToDouble(sum);
                        }
			            case NPTypeCode.Single: 
                        {
                            var iter = arr.AsIterator<float>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Single sum = 0f;
                            while (hasNext())
                                sum += (Single) moveNext();

                            return Convert.ToSingle(sum);
                        }
			            case NPTypeCode.Decimal: 
                        {
                            var iter = arr.AsIterator<float>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Decimal sum = 0m;
                            while (hasNext())
                                sum += (Decimal) moveNext();

                            return Convert.ToDecimal(sum);
                        }
			            default:
				            throw new NotSupportedException();
		            }
                    break;
                }
			    case NPTypeCode.Decimal: 
                {
                    switch (retType)
		            {
			            case NPTypeCode.Byte: 
                        {
                            var iter = arr.AsIterator<decimal>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt32 sum = 0;
                            while (hasNext())
                                sum += (UInt32) moveNext();

                            return Convert.ToByte(sum);
                        }
			            case NPTypeCode.Int16: 
                        {
                            var iter = arr.AsIterator<decimal>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Int32 sum = 0;
                            while (hasNext())
                                sum += (Int32) moveNext();

                            return Convert.ToInt16(sum);
                        }
			            case NPTypeCode.UInt16: 
                        {
                            var iter = arr.AsIterator<decimal>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt32 sum = 0;
                            while (hasNext())
                                sum += (UInt32) moveNext();

                            return Convert.ToUInt16(sum);
                        }
			            case NPTypeCode.Int32: 
                        {
                            var iter = arr.AsIterator<decimal>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Int32 sum = 0;
                            while (hasNext())
                                sum += (Int32) moveNext();

                            return Convert.ToInt32(sum);
                        }
			            case NPTypeCode.UInt32: 
                        {
                            var iter = arr.AsIterator<decimal>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt32 sum = 0u;
                            while (hasNext())
                                sum += (UInt32) moveNext();

                            return Convert.ToUInt32(sum);
                        }
			            case NPTypeCode.Int64: 
                        {
                            var iter = arr.AsIterator<decimal>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Int64 sum = 0L;
                            while (hasNext())
                                sum += (Int64) moveNext();

                            return Convert.ToInt64(sum);
                        }
			            case NPTypeCode.UInt64: 
                        {
                            var iter = arr.AsIterator<decimal>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt64 sum = 0UL;
                            while (hasNext())
                                sum += (UInt64) moveNext();

                            return Convert.ToUInt64(sum);
                        }
			            case NPTypeCode.Char: 
                        {
                            var iter = arr.AsIterator<decimal>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            UInt32 sum = '\0';
                            while (hasNext())
                                sum += (UInt32) moveNext();

                            return Convert.ToChar(sum);
                        }
			            case NPTypeCode.Double: 
                        {
                            var iter = arr.AsIterator<decimal>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Double sum = 0d;
                            while (hasNext())
                                sum += (Double) moveNext();

                            return Convert.ToDouble(sum);
                        }
			            case NPTypeCode.Single: 
                        {
                            var iter = arr.AsIterator<decimal>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Single sum = 0f;
                            while (hasNext())
                                sum += (Single) moveNext();

                            return Convert.ToSingle(sum);
                        }
			            case NPTypeCode.Decimal: 
                        {
                            var iter = arr.AsIterator<decimal>();
                            var moveNext = iter.MoveNext;
                            var hasNext = iter.HasNext;
                            Decimal sum = 0m;
                            while (hasNext())
                                sum += (Decimal) moveNext();

                            return Convert.ToDecimal(sum);
                        }
			            default:
				            throw new NotSupportedException();
		            }
                    break;
                }
			    default:
				    throw new NotSupportedException();
		    }
            #endregion
#endif
        }
    }
}
