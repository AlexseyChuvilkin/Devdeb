﻿using System;

namespace Devdeb.Serialization.Serializers.System
{
	public class GuidSerializer : ConstantLengthSerializer<Guid>
	{
		public GuidSerializer() : base(16) { }

		public unsafe override void Serialize(Guid instance, byte[] buffer, int offset)
		{
			VerifySerialize(instance, buffer, offset);
			fixed (byte* bufferPointer = &buffer[offset])
				*(Guid*)bufferPointer = instance;
		}
		public unsafe override Guid Deserialize(byte[] buffer, int offset)
		{
			VerifyDeserialize(buffer, offset);
			fixed (byte* bufferPointer = &buffer[offset])
				return *(Guid*)bufferPointer;
		}
	}
}
