using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace SuperiorArrays {


    class Sray
    {

        #region CleanArray

        //Game Object
        public static GameObject[] CleanArray(GameObject[] gArray)
        {
            GameObject[] tempArray = new GameObject[0];
            for (int i = 0; i < gArray.Length; i++)
            {
                if (gArray[i] != null)
                {
                    tempArray = AppendArray(tempArray, gArray[i]);
                }
            }
            return tempArray;


        }

        //Vector3
        public static Vector3[] CleanArray(Vector3[] gArray)
        {
            Vector3[] tempArray = new Vector3[0];
            for (int i = 0; i < gArray.Length; i++)
            {
                if (gArray[i] != null)
                {
                    tempArray = AppendArray(tempArray, gArray[i]);
                }
            }
            return tempArray;


        }

        #endregion

        #region DeactivateArray

        //GameObject
        public static void DeactivateArray(GameObject[] gArray)
        {        
            for (int i = 0; i < gArray.Length; i++)
            {
                gArray[i].SetActive(false);
            }
        }


        #endregion

        #region AppendArray

        //String
        public static string[] AppendArray(string[] sArray, string addition)
        {
            string[] tempArray = new string[sArray.Length + 1];
            for (int i = 0; i < sArray.Length; i++)
            {
                tempArray[i] = sArray[i];
            }
            tempArray[tempArray.Length - 1] = addition;
            return tempArray;
        }

        //Sprite
        public static Sprite[] AppendArray(Sprite[] sArray, Sprite addition)
        {
            Sprite[] tempArray = new Sprite[sArray.Length + 1];
            for (int i = 0; i < sArray.Length; i++)
            {
                tempArray[i] = sArray[i];
            }
            tempArray[tempArray.Length - 1] = addition;
            return tempArray;
        }


        //Float
        public static float[] AppendArray(float[] fArray, float addition)
        {
            float[] tempArray = new float[fArray.Length + 1];
            for (int i = 0; i < fArray.Length; i++)
            {
                tempArray[i] = fArray[i];
            }
            tempArray[tempArray.Length - 1] = addition;
            return tempArray;
        }

        //Int
        public static int[] AppendArray(int[] fArray, int addition)
        {
            int[] tempArray = new int[fArray.Length + 1];
            for (int i = 0; i < fArray.Length; i++)
            {
                tempArray[i] = fArray[i];
            }
            tempArray[tempArray.Length - 1] = addition;
            return tempArray;
        }

        //GameObject
        public static GameObject[] AppendArray(GameObject[] gArray, GameObject addition)
        {
            GameObject[] tempArray = new GameObject[gArray.Length + 1];
            for (int i = 0; i < gArray.Length; i++)
            {
                tempArray[i] = gArray[i];
            }
            tempArray[tempArray.Length - 1] = addition;
            return tempArray;
        }

        //Vector3
        public static Vector3[] AppendArray(Vector3[] gArray, Vector3 addition)
        {
            Vector3[] tempArray = new Vector3[gArray.Length + 1];
            for (int i = 0; i < gArray.Length; i++)
            {
                tempArray[i] = gArray[i];
            }
            tempArray[tempArray.Length - 1] = addition;
            return tempArray;
        }

        //Mesh
        public static Mesh[] AppendArray(Mesh[] gArray, Mesh addition)
        {
            Mesh[] tempArray = new Mesh[gArray.Length + 1];
            for (int i = 0; i < gArray.Length; i++)
            {
                tempArray[i] = gArray[i];
            }
            tempArray[tempArray.Length - 1] = addition;
            return tempArray;
        }

        //Render Texture
        public static RenderTexture[] AppendArray(RenderTexture[] gArray, RenderTexture addition)
        {
            RenderTexture[] tempArray = new RenderTexture[gArray.Length + 1];
            for (int i = 0; i < gArray.Length; i++)
            {
                tempArray[i] = gArray[i];
            }
            tempArray[tempArray.Length - 1] = addition;
            return tempArray;
        }
        #endregion

        #region RemoveInArray
        //GameObject
        public static GameObject[] RemoveInArray(GameObject[] gArray, GameObject deletion)
        {
            GameObject[] tempArray = new GameObject[0];
            for (int i = 0; i < gArray.Length; i++)
            {
                if (gArray[i] != deletion)
                {
                    tempArray = AppendArray(tempArray, gArray[i]);
                }
            }
            return tempArray;
        }

        //Vector3
        public static Vector3[] RemoveInArray(Vector3[] gArray, Vector3 deletion)
        {
            Vector3[] tempArray = new Vector3[0];
            for (int i = 0; i < gArray.Length; i++)
            {
                if (gArray[i] != deletion)
                {
                    tempArray = AppendArray(tempArray, gArray[i]);
                }
            }
            return tempArray;
        }
        #endregion

        #region CheckInArray
        //GameObject
        public static bool CheckInArray(GameObject[] gArray, GameObject gObject)
        {

            for (int i = 0; i < gArray.Length; i++)
            {
                if (gArray[i] == gObject)
                {
                    return true;
                }
            }



            return false;
        }

        //int
        public static bool CheckInArray(int[] gArray, int gObject)
        {

            for (int i = 0; i < gArray.Length; i++)
            {
                if (gArray[i] == gObject)
                {
                    return true;
                }
            }



            return false;
        }


        #endregion


    }










}
