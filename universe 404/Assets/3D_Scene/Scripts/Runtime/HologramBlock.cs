using System;

namespace UnityEngine.Rendering.Universal
{
    [System.Serializable, VolumeComponentMenu("HL/HologramBlock")]
    public sealed class HologramBlock : VolumeComponent, IPostProcessComponent
    {
        [Tooltip("是否开启效果")]
        public BoolParameter enableEffect = new BoolParameter(true);

        [Range(0f,1f),Tooltip("全息投影的扫描线抖动")]
        public FloatParameter scanLineJitter = new FloatParameter(0.17f);

        [Range(0f, 1f), Tooltip("全息投影的扫描线颜色抖动")]
        public FloatParameter colorDrift = new FloatParameter(0f);

        [Range(0f, 100f), Tooltip("全息投影的错误视频速度")]
        public FloatParameter speed = new FloatParameter(0f);

        [Range(0f, 100f), Tooltip("全息投影的错误视频方块大小")]
        public FloatParameter blockSize = new FloatParameter(5f);

        public bool IsActive() => enableEffect==true;

        public bool IsTileCompatible() => false;
    }
}
