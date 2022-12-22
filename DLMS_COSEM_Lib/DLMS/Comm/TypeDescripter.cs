using System;
using System.Collections.Generic;
using System.Text;

namespace DLMS.Comm
{
    public class TypeDescriptor : IDisposable
    {
        #region Data_Members

        private sbyte sequenceId;
        private ushort numberOfElements;
        private DataTypes typeTAG;
        private object parent;
        private List<TypeDescriptor> elements;

        #endregion

        #region Properties

        public ushort NumberOfElements
        {
            get { return numberOfElements; }
            internal set { numberOfElements = value; }
        }

        public DataTypes TypeTAG
        {
            get { return typeTAG; }
            internal set { typeTAG = value; }
        }

        public sbyte SequenceId
        {
            get { return sequenceId; }
            internal set { sequenceId = value; }
        }

        public object Parent
        {
            get
            {
                return parent;
            }
            internal set
            {
                parent = value;
            }
        }

        public List<TypeDescriptor> Elements
        {
            get { return elements; }
            internal set { elements = value; }
        }

        #endregion

        #region Constructors

        public TypeDescriptor(int dummyParameter = 0)
        {
            sequenceId = 0;
            numberOfElements = 0;
            typeTAG = DataTypes._A00_Null;
            parent = null;
            elements = null;
        }

        public TypeDescriptor(object ParentArg, DataTypes TypeTAGArg)
        {
            sequenceId = 0;
            numberOfElements = 0;
            typeTAG = TypeTAGArg;
            parent = ParentArg;
            elements = null;
        }

        #endregion

        #region Member_Methods

        public bool AddSubTypeDescripter(TypeDescriptor typeDescripter, sbyte SequenceId = -1)
        {
            bool retVal = false;
            try
            {
                if (Elements == null)
                    Elements = new List<TypeDescriptor>();

                // Add End Of The List
                if (SequenceId <= 0)
                {
                    if (Elements.Count > 1)
                    {
                        SequenceId = Elements[Elements.Count - 1].SequenceId;
                        SequenceId++;
                    }

                    if (SequenceId == 0)
                        SequenceId = 1;

                    typeDescripter.SequenceId = SequenceId;
                    Elements.Add(typeDescripter);

                    retVal = true;
                    return retVal;
                }
                // Add In Pre-Defined Specific Index Location
                else if (SequenceId >= 1)
                {
                    // Dispose Previous Obj On Index Location
                    TypeDescriptor Obj = new TypeDescriptor(); //;
                    int element_Index = Elements.FindIndex((x) => x.SequenceId > 0 && x.SequenceId == SequenceId);
                    if (element_Index >= 0)
                        Obj = Elements[element_Index];

                    if (Obj.SequenceId == SequenceId &&
                        Elements.Contains(Obj))
                    {
                        Elements.Remove(Obj);
                        Obj.Dispose();
                    }

                    typeDescripter.SequenceId = SequenceId;
                    Elements.Add(typeDescripter);

                    retVal = true;
                    return retVal;
                }
            }
            catch
            {
                // Log_ErrorMessage
                retVal = false;
            }
            finally
            {
                if (Elements.Count > 1)
                    // Sort Elements Based On SequenceId
                    Elements.Sort(new System.Comparison<TypeDescriptor>((TypeDescriptor ObjA, TypeDescriptor ObjB) =>
                    { return ObjA.SequenceId.CompareTo(ObjB.SequenceId); }));
            }

            return false;
        }

        public bool RemoveSubTypeDescripter(TypeDescriptor typeDescripter, sbyte SequenceId = -1)
        {
            bool retVal = false;
            try
            {
                if (Elements == null)
                    Elements = new List<TypeDescriptor>();

                int element_Index = -1;

                // Add End Of The List
                if (SequenceId <= 0)
                {
                    element_Index = Elements.FindIndex((x) => x.SequenceId > 0 && x.SequenceId == SequenceId);
                }
                // Add In Pre-Defined Specific Index Location
                else if (SequenceId >= 1)
                {
                    // Dispose Previous Obj On Index Location
                    if (typeDescripter.SequenceId > 0)
                        // Only Compare SeqId Param & typeDescripter 
                        element_Index = Elements.FindIndex((x) => x.SequenceId > 0 && x.SequenceId == SequenceId && x.Equals(typeDescripter));
                    else
                        // Only Compare SeqId Param
                        element_Index = Elements.FindIndex((x) => x.SequenceId > 0 && x.SequenceId == SequenceId);
                }

                TypeDescriptor Obj = new TypeDescriptor();
                if (Elements.Count > 0 && element_Index >= 0)
                {
                    Obj = Elements[element_Index];
                }
                retVal = Elements.Remove(Obj);
            }
            catch
            {
                // Log_ErrorMessage
                retVal = false;
            }

            return false;
        }

        #endregion

        #region Ext_Function

        public IEnumerable<TypeDescriptor> All()
        {
            return TypeDescripterExt.All(this);
        }

        public TypeDescriptor Find(Predicate<TypeDescriptor> match)
        {
            if (match.Invoke(this))
                return this;
            else
                return TypeDescripterExt.Find(this.Elements, match);
        }

        public IEnumerable<TypeDescriptor> FindAll(Predicate<TypeDescriptor> match)
        {
            List<TypeDescriptor> Itr = new List<TypeDescriptor>();

            if (match.Invoke(this))
                Itr.Add(this);

            var lst = TypeDescripterExt.FindAll(this.Elements, match);

            foreach (var item in lst)
            {
                Itr.Add(item);
            }

            return Itr;
        }

        #endregion

        public void Dispose()
        {
            if (elements != null && elements.Count > 0)
                for (int index = 0; index < elements.Count; )
                {
                    TypeDescriptor Obj = elements[index];
                    if (Obj.SequenceId > 0 && elements.Contains(Obj))
                    {
                        elements.Remove(Obj);
                        Obj.sequenceId = -1;
                        Obj.Parent = null;
                        Obj.typeTAG = DataTypes._A00_Null;
                        Obj.numberOfElements = 0;

                        Obj.Dispose();
                    }
                    else
                        index++;
                }
        }

        /// <summary>
        /// Returns the String representation of the TypeDescripter object
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            String baseStr = base.ToString();
            StringBuilder strVal = new StringBuilder();
            IEnumerable<TypeDescriptor> TypeDescripterCollection = this.All();

            try
            {
                foreach (TypeDescriptor typeDescripter in TypeDescripterCollection)
                {
                    if (typeDescripter == null)
                        continue;

                    // Print_TypeDescripter
                    if (typeDescripter.SequenceId >= 1)
                        strVal.AppendFormat("ID:{0:X} {1:X} ", typeDescripter.SequenceId, typeDescripter.TypeTAG);
                    else
                        strVal.AppendFormat("{0:X} ", typeDescripter.TypeTAG);

                    if (typeDescripter.TypeTAG == DataTypes._A01_array ||
                        typeDescripter.TypeTAG == DataTypes._A02_structure)
                    {
                        strVal.AppendFormat("{0:X} ", typeDescripter.NumberOfElements);
                        strVal.AppendLine();
                    }
                }
            }
            catch (Exception ex)
            {
                strVal.AppendFormat("Error Print Type Descriptor", ex.Message);
            }

            return baseStr + strVal;
        }
    }

    public static class TypeDescripterExt
    {
        #region Ext__Function

        /// <summary>
        /// Provide IEnumerator For DepthWise Traversal Elements of Structure
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static IEnumerable<TypeDescriptor> All(this TypeDescriptor node)
        {
            if (node != null && node.Parent == null)
                yield return node;

            // Node Sub-Elements
            if (node.Elements != null &&
                node.Elements.Count > 0)
            {
                foreach (TypeDescriptor n in node.Elements)
                {
                    yield return n;

                    // Recursive Calls To All
                    if (n.Elements != null && n.Elements.Count > 0)
                        foreach (TypeDescriptor child in TypeDescripterExt.All(n))
                            yield return child;
                }
            }
        }

        public static TypeDescriptor Find(IEnumerable<TypeDescriptor> nodes, Predicate<TypeDescriptor> match)
        {
            if (nodes != null)
            {
                foreach (TypeDescriptor n in nodes)
                {
                    if (match.Invoke(n))
                        return n;
                    else
                        continue;
                }
                // Level-2 Search
                foreach (TypeDescriptor n in nodes)
                {
                    if (n.Elements != null && n.Elements.Count > 0)
                        return TypeDescripterExt.Find(n.Elements, match);
                }
            }
            // Return Default Obj
            return new TypeDescriptor();
        }


        public static IEnumerable<TypeDescriptor> FindAll(IEnumerable<TypeDescriptor> nodes, Predicate<TypeDescriptor> match)
        {
            if (nodes != null)
            {
                foreach (TypeDescriptor n in nodes)
                {
                    if (match.Invoke(n))
                        yield return n;
                    else
                        continue;
                }

                // Level-2 Search
                foreach (TypeDescriptor n in nodes)
                {
                    if (n.Elements != null && n.Elements.Count > 0)
                        yield return TypeDescripterExt.Find(n.Elements, match);
                }
            }
        }


        #endregion
    }
}
