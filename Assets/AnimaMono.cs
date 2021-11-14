using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimaMono : MonoBehaviour
{
    public string CurrentAnima;
    public GameObject prefab;
    public List<AnimaModel> animaModels;

    void OnValidate ()
    {
        animaModels.Start();
    }
    // Start is called before the first frame update
    void Start()
    {
        animaModels.Start();
    }
    // Update is called once per frame
    void Update()
    {
        var w = animaModels.Find(n => n.name == CurrentAnima);
        w.Update(prefab, transform);
    }
}
public static class AnimaModelsMethods
{
    public const string Animacion_Ojo = "Animacion_Ojo";
    public const string Animacion_Idle = "Animacion_Idle";
    public const string Animacion_Monster = "Animacion_Monster";
    public const string Animacion_Run = "Animacion_Run";
    
    public static void Start (this List<AnimaModel> l)
    {
        if(l.Count != 0) { return; }

        l.Add(new AnimaModel() { name = Animacion_Ojo});
        l.Add(new AnimaModel() { name = Animacion_Idle});
        l.Add(new AnimaModel() { name = Animacion_Monster});
        l.Add(new AnimaModel() { name = Animacion_Run});
    }
    public static AnimaModel Anima_Ojo(this List<AnimaModel> l)
    {
        return l.Find(n => n.name == Animacion_Ojo);
    }
    public static AnimaModel Anima_Idle(this List<AnimaModel> l)
    {
        return l.Find(n => n.name == Animacion_Idle);
    }
    public static AnimaModel Anima_Monster(this List<AnimaModel> l)
    {
        return l.Find(n => n.name == Animacion_Monster);
    }
    public static AnimaModel Anima_Run(this List<AnimaModel> l)
    {
        return l.Find(n => n.name == Animacion_Run);
    }
    public static void DestroyAll (this Transform l)
    {
        List<Transform> r = new List<Transform>();
        foreach(Transform c in l)
        {
            r.Add(c);
        }
        r.ForEach(n => GameObject.Destroy(n.gameObject, 0f));
    }
}
[System.Serializable]
public class AnimaModel
{
    public string name;
    public AnimationClip CurrentAnimation;
    public Animation AnimaSystem;

    public void Update (GameObject prefab, Transform parent)
    {
        if(AnimaSystem == null)
        {
            Spawn(prefab, parent);
        }
        else
        {
            if(AnimaSystem.isPlaying)
            {
            }
            else
            {
                Spawn(prefab, parent);
            }
        }
    }
    private void Spawn (GameObject prefab, Transform parent)
    {
        parent.DestroyAll();

        var c = GameObject.Instantiate(prefab, parent);
        c.transform.localPosition = new Vector3(0f,0f,0f);
        c.transform.localRotation = Quaternion.Euler(0f,0f,0f);
            
        AnimaSystem = c.GetComponent<Animation>();
        AnimaSystem.clip = CurrentAnimation;
        AnimaSystem.Play();
    }
}