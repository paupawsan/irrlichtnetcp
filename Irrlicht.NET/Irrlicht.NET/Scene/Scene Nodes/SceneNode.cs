using System;
using System.Runtime.InteropServices;

namespace IrrlichtNETCP
{
    public class SceneNode : NativeElement
    {
        public SceneNode(IntPtr raw)
            : base(raw)
        {
        }

        public SceneNode() : base()
        {
        }

        public virtual Vector3D AbsolutePosition
        {
            get
            {
                float[] abs = new float[3];
                SceneNode_GetAbsolutePosition(_raw, abs);
                return Vector3D.FromUnmanaged(abs);
            }
        }

        public virtual Matrix4 AbsoluteTransformation
        {
            get
            {
                float[] mat = new float[16];
                SceneNode_GetAbsoluteTransformation(_raw, mat);
                return Matrix4.FromUnmanaged(mat);
            }
        }

        public bool AutomaticCulling
        {
            get
            {
                return SceneNode_GetAutomaticCulling(_raw);
            }
            set
            {
                SceneNode_SetAutomaticCulling(_raw, value);
            }
        }

        public virtual Box3D BoundingBox
        {
            get
            {
                float[] box = new float[6];
                SceneNode_GetBoundingBox(_raw, box);
                return Box3D.FromUnmanaged(box);
            }
        }

        public SceneNode[] Children
        {
            get
            {
                uint size = SceneNode_GetChildrenCount(_raw);
                IntPtr[] rawlist = new IntPtr[size];
                SceneNode_GetChildren(_raw, rawlist);
                SceneNode[] tor = new SceneNode[rawlist.Length];
                for (int i = 0; i < rawlist.Length; i++)
                    tor[i] = (SceneNode)NativeElement.GetObject(rawlist[i], typeof(SceneNode));
                return tor;
            }
        }

        public virtual int ID
        {
            get
            {
                return SceneNode_GetID(_raw);
            }
            set
            {
                SceneNode_SetID(_raw, value);
            }
        }

        public virtual int MaterialCount
        {
            get
            {
                return SceneNode_GetMaterialCount(_raw);
            }
        }

        public virtual SceneNode Parent
        {
            get
            {
                return (SceneNode)
                    NativeElement.GetObject(SceneNode_GetParent(_raw),
                                            typeof(SceneNode));
            }
            set
            {
                if (value != null)
                    SceneNode_SetParent(_raw, value.Raw);
                else
                    SceneNode_SetParent(_raw, IntPtr.Zero);
            }
        }

        public string Name
        {
            get
            {
                return SceneNode_GetName(_raw);
            }
            set
            {
                SceneNode_SetName(_raw, value);
            }
        }

        public virtual Vector3D Position
        {
            get
            {
                float[] pos = new float[3];
                SceneNode_GetPosition(_raw, pos);
                return Vector3D.FromUnmanaged(pos);
            }
            set
            {
                SceneNode_SetPosition(_raw, value.ToUnmanaged());
            }
        }

        public virtual Vector3D Rotation
        {
            get
            {
                float[] rot = new float[3];
                SceneNode_GetRotation(_raw, rot);
                return Vector3D.FromUnmanaged(rot);
            }
            set
            {
                SceneNode_SetRotation(_raw, value.ToUnmanaged());
            }
        }

        public virtual Vector3D Scale
        {
            get
            {
                float[] scale = new float[3];
                SceneNode_GetScale(_raw, scale);
                return Vector3D.FromUnmanaged(scale);
            }
            set
            {
                SceneNode_SetScale(_raw, value.ToUnmanaged());
            }
        }

        public virtual Matrix4 RelativeTransformation
        {
            get
            {
                float[] mat = new float[16];
                SceneNode_GetRelativeTransformation(_raw, mat);
                return Matrix4.FromUnmanaged(mat);
            }
        }

        public virtual Box3D TransformedBoundingBox
        {
            get
            {
                float[] box = new float[6];
                SceneNode_GetTransformedBoundingBox(_raw, box);
                return Box3D.FromUnmanaged(box);
            }
        }

        public virtual TriangleSelector TriangleSelector
        {
            get
            {
                return (TriangleSelector)
                    NativeElement.GetObject(SceneNode_GetTriangleSelector(_raw),
                                            typeof(TriangleSelector));
            }
            set
            {
                SceneNode_SetTriangleSelector(_raw, value.Raw);
            }
        }

        public SceneNodeType SceneNodeType
        {
            get
            {
                return SceneNode_GetType(_raw);
            }
        }

        public bool DebugDataVisible
        {
            get
            {
                return SceneNode_IsDebugDataVisible(_raw);
            }
            set
            {
                SceneNode_SetDebugDataVisible(_raw, value);
            }
        }

        public bool DebugObject
        {
            get
            {
                return SceneNode_IsDebugObject(_raw);
            }
            set
            {
                SceneNode_SetIsDebugObject(_raw, value);
            }
        }

        public virtual bool Visible
        {
            get
            {
                return SceneNode_IsVisible(_raw);
            }
            set
            {
                SceneNode_SetVisible(_raw, value);
            }
        }

        public virtual void AddAnimator(Animator animator)
        {
            SceneNode_AddAnimator(_raw, animator.Raw);
        }

        public virtual void AddChild(SceneNode child)
        {
            SceneNode_AddChild(_raw, child.Raw);
        }

        public virtual Material GetMaterial(int i)
        {
            return (Material)
                NativeElement.GetObject(SceneNode_GetMaterial(_raw, i),
                                        typeof(Material));
        }

        public override void Dispose()
        {
            if (Raw != IntPtr.Zero)
                this.Remove();
            base.Dispose();
        }

        public virtual void OnPostRender(uint timeMS)
        {
            SceneNode_OnPostRender(_raw, timeMS);
        }

        public virtual void OnPreRender()
        {
            SceneNode_OnPreRender(_raw);
        }

        public virtual void Remove()
        {
            SceneNode_Remove(_raw);
        }

        public virtual void RemoveAll()
        {
            SceneNode_RemoveAll(_raw);
        }

        public virtual void RemoveAnimator(Animator anim)
        {
            SceneNode_RemoveAnimator(_raw, anim.Raw);
        }

        public virtual void RemoveAnimators()
        {
            SceneNode_RemoveAnimators(_raw);
        }

        public virtual void RemoveChild(SceneNode child)
        {
            SceneNode_RemoveChild(_raw, child.Raw);
        }

        public virtual void Render()
        {
            SceneNode_Render(_raw);
        }

        public void SetMaterialFlag(MaterialFlag flag, bool val)
        {
            SceneNode_SetMaterialFlag(_raw, flag, val);
        }

        public void SetMaterialTexture(int layer, Texture text)
        {
            SceneNode_SetMaterialTexture(_raw, layer, (text == null ? IntPtr.Zero : text.Raw));
        }

        public void SetMaterialType(MaterialType type)
        {
            SceneNode_SetMaterialType(_raw, type);
        }

        public void SetMaterialType(int type)
        {
            SetMaterialType((MaterialType)type);
        }

        public virtual void UpdateAbsolutePosition()
        {
            SceneNode_UpdateAbsolutePosition(_raw);
        }


        #region .NET Wrapper Native Code
        [DllImport(Native.Dll)]
        static extern void SceneNode_AddAnimator(IntPtr scenenode, IntPtr animator);

        [DllImport(Native.Dll)]
        static extern void SceneNode_AddChild(IntPtr scenenode, IntPtr childnode);

        [DllImport(Native.Dll)]
        static extern void SceneNode_GetAbsolutePosition(IntPtr scenenode, [MarshalAs(UnmanagedType.LPArray)] float[] toR);

        [DllImport(Native.Dll)]
        static extern void SceneNode_GetAbsoluteTransformation(IntPtr scenenode, [MarshalAs(UnmanagedType.LPArray)] float[] toR);

        [DllImport(Native.Dll)]
        static extern void SceneNode_GetChildren(IntPtr scenenode, [MarshalAs(UnmanagedType.LPArray)] IntPtr[] list);

        [DllImport(Native.Dll)]
        static extern uint SceneNode_GetChildrenCount(IntPtr scenenode);

        [DllImport(Native.Dll)]
        static extern bool SceneNode_GetAutomaticCulling(IntPtr scenenode);

        [DllImport(Native.Dll)]
        static extern void SceneNode_GetBoundingBox(IntPtr scenenode, [MarshalAs(UnmanagedType.LPArray)] float[] toR);

        [DllImport(Native.Dll)]
        static extern int SceneNode_GetID(IntPtr scenenode);

        [DllImport(Native.Dll)]
        static extern int SceneNode_GetMaterialCount(IntPtr scenenode);

        [DllImport(Native.Dll)]
        static extern IntPtr SceneNode_GetMaterial(IntPtr scenenode, int i);

        [DllImport(Native.Dll)]
        static extern IntPtr SceneNode_GetParent(IntPtr scenenode);

        [DllImport(Native.Dll)]
        static extern string SceneNode_GetName(IntPtr scenenode);

        [DllImport(Native.Dll)]
        static extern void SceneNode_GetPosition(IntPtr scenenode, [MarshalAs(UnmanagedType.LPArray)] float[] toR);

        [DllImport(Native.Dll)]
        static extern void SceneNode_GetRelativeTransformation(IntPtr scenenode, [MarshalAs(UnmanagedType.LPArray)] float[] toR);

        [DllImport(Native.Dll)]
        static extern void SceneNode_GetRotation(IntPtr scenenode, [MarshalAs(UnmanagedType.LPArray)] float[] toR);

        [DllImport(Native.Dll)]
        static extern void SceneNode_GetScale(IntPtr scenenode, [MarshalAs(UnmanagedType.LPArray)] float[] toR);

        [DllImport(Native.Dll)]
        static extern void SceneNode_GetTransformedBoundingBox(IntPtr scenenode, float[] toR);

        [DllImport(Native.Dll)]
        static extern IntPtr SceneNode_GetTriangleSelector(IntPtr scenenode);

        [DllImport(Native.Dll)]
        static extern SceneNodeType SceneNode_GetType(IntPtr scenenode);

        [DllImport(Native.Dll)]
        static extern bool SceneNode_IsDebugDataVisible(IntPtr scenenode);

        [DllImport(Native.Dll)]
        static extern bool SceneNode_IsDebugObject(IntPtr scenenode);

        [DllImport(Native.Dll)]
        static extern bool SceneNode_IsVisible(IntPtr scenenode);

        [DllImport(Native.Dll)]
        static extern void SceneNode_OnPostRender(IntPtr scenenode, uint timeMS);

        [DllImport(Native.Dll)]
        static extern void SceneNode_OnPreRender(IntPtr scenenode);

        [DllImport(Native.Dll)]
        static extern void SceneNode_Remove(IntPtr scenenode);

        [DllImport(Native.Dll)]
        static extern void SceneNode_RemoveAll(IntPtr scenenode);

        [DllImport(Native.Dll)]
        static extern void SceneNode_RemoveAnimator(IntPtr scenenode, IntPtr animator);

        [DllImport(Native.Dll)]
        static extern void SceneNode_RemoveAnimators(IntPtr scenenode);

        [DllImport(Native.Dll)]
        static extern bool SceneNode_RemoveChild(IntPtr scenenode, IntPtr childscenenode);

        [DllImport(Native.Dll)]
        static extern void SceneNode_Render(IntPtr scenenode);

        [DllImport(Native.Dll)]
        static extern void SceneNode_SetAutomaticCulling(IntPtr scenenode, bool enabled);

        [DllImport(Native.Dll)]
        static extern void SceneNode_SetDebugDataVisible(IntPtr scenenode, bool visible);

        [DllImport(Native.Dll)]
        static extern void SceneNode_SetID(IntPtr scenenode, int id);

        [DllImport(Native.Dll)]
        static extern void SceneNode_SetIsDebugObject(IntPtr scenenode, bool debugObject);

        [DllImport(Native.Dll)]
        static extern void SceneNode_SetMaterialFlag(IntPtr scenenode, MaterialFlag flag, bool newvalue);

        [DllImport(Native.Dll)]
        static extern void SceneNode_SetMaterialTexture(IntPtr scenenode, int layer, IntPtr texture);

        [DllImport(Native.Dll)]
        static extern void SceneNode_SetMaterialType(IntPtr scenenode, MaterialType newtype);

        [DllImport(Native.Dll)]
        static extern void SceneNode_SetName(IntPtr scenenode, string name);

        [DllImport(Native.Dll)]
        static extern void SceneNode_SetParent(IntPtr scenenode, IntPtr parent);

        [DllImport(Native.Dll)]
        static extern void SceneNode_SetPosition(IntPtr scenenode, float[] pos);

        [DllImport(Native.Dll)]
        static extern void SceneNode_SetRotation(IntPtr scenenode, float[] rot);

        [DllImport(Native.Dll)]
        static extern void SceneNode_SetScale(IntPtr scenenode, float[] scale);

        [DllImport(Native.Dll)]
        static extern void SceneNode_SetTriangleSelector(IntPtr scenenode, IntPtr triangleselector);

        [DllImport(Native.Dll)]
        static extern void SceneNode_SetVisible(IntPtr scenenode, bool visible);

        [DllImport(Native.Dll)]
        static extern void SceneNode_UpdateAbsolutePosition(IntPtr scenenode);
        #endregion
    }
}
