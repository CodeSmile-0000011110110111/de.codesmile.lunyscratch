#if GODOT
using Godot;
using System;

namespace LunyScratch
{
	internal sealed class GodotEngineObject : IEngineObject
	{
		private readonly GodotObject _engineObject;

		public static implicit operator GodotEngineObject(GodotObject engineObject) => new(engineObject);

		public GodotEngineObject(GodotObject engineObject) => _engineObject = engineObject;

		public void SetEnabled(Boolean enabled)
		{
			switch (_engineObject)
			{
				case CanvasItem canvasItem:
					canvasItem.Visible = enabled;
					break;
				case Light3D light3D:
					light3D.Visible = enabled;
					break;
				case CollisionObject3D collision:
					collision.ProcessMode = enabled ? Node.ProcessModeEnum.Inherit : Node.ProcessModeEnum.Disabled;
					break;

				case Node node:
					node.ProcessMode = enabled ? Node.ProcessModeEnum.Inherit : Node.ProcessModeEnum.Disabled;
					break;
			}
		}
	}
#endif
}
