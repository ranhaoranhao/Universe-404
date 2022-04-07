using System.Linq;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace UnityEditor.Rendering.Universal
{
    [VolumeComponentEditor(typeof(HologramBlock))]
    sealed class HologramBlockEditor : VolumeComponentEditor
    {
        SerializedDataParameter m_enableEffect;
        SerializedDataParameter m_scanLineJitter;
        SerializedDataParameter m_colorDrift;
        SerializedDataParameter m_speed;
        SerializedDataParameter m_blockSize;

        public override void OnEnable()
        {
            var o = new PropertyFetcher<HologramBlock>(serializedObject);

            m_scanLineJitter = Unpack(o.Find(x => x.scanLineJitter));
            m_colorDrift = Unpack(o.Find(x => x.colorDrift));
            m_speed = Unpack(o.Find(x => x.speed));
            m_blockSize = Unpack(o.Find(x => x.blockSize));
            m_enableEffect = Unpack(o.Find(x => x.enableEffect));
        }

        //public override void OnDisable()
        //{
        //    var o = new PropertyFetcher<HologramBlock>(serializedObject);
        //    m_enableEffect = Unpack(o.Find(x => x.enableEffect));
        //    HologramBlock.en.value = false;
        //}

        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("全息效果及视频错误效果", EditorStyles.largeLabel);
            EditorGUILayout.LabelField("请勿手动禁用或移除该效果，代码会处理", EditorStyles.miniLabel);
            EditorGUILayout.LabelField("要勾选Enable Effect才能生效，请用这个切换开启关闭效果。", EditorStyles.miniLabel);
            PropertyField(m_enableEffect);
            PropertyField(m_scanLineJitter);
            PropertyField(m_colorDrift);
            PropertyField(m_speed);
            PropertyField(m_blockSize);
        }
    }
}
