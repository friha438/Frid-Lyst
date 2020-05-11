using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sats1_3 : MonoBehaviour{


        Renderer rend;

        Vector4 color1;
        Vector4 color2;
        Vector4 color3= new Vector4(0f,0f,0f,1f);
        Vector4 color4;
        Vector4 color_w=new Vector4(1f,1f,1f,1f);
        Vector4 color_b= new Vector4(0f,0f,0f,1f);
        Vector4 color_c=new Vector4(1f,1f,1f,1f);
        Vector4 color=new Vector4(1f,1f,1f,1f);

        float fire=0f;
        float fire_g=0f;

        float time1=0f;
        float time2=0f;

        float b_1;
        float b_2;

        Shader shader_back;
        Shader shader_star;
        Shader shader_moln;
        Shader shader_eld;

        float m=0.009f;

        float newsize2=0f;

        float trans=0f;

        float speed=0.05f;
        float speed1=0.05f;
        float speed_c=0.002f;
        float sats=0f;
        float speed_s=0.006f;
        float time_c=0f;

        float n=0f;

        float newSize1=0;

        // Start is called before the first frame update
        void Start()
        {
            shader_back = Shader.Find("Unlit/background");
            shader_star = Shader.Find("Unlit/stars");
            shader_moln= Shader.Find("Unlit/moln");
            shader_eld= Shader.Find("Unlit/fire2");
            rend = GetComponent<Renderer> ();

            if(rend.material.shader == shader_moln){

                rend.material.SetFloat("_c", 0.14f);
                rend.material.SetFloat("_a", 0.014f);
                rend.material.SetFloat("_t", 0f);
                rend.material.SetFloat("_b", 0.4f);
                rend.material.SetFloat("_d", 2.13f);
                rend.material.SetFloat("_size", 3.6f);
                rend.material.SetFloat("_t", 0f);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if(rend.material.shader == shader_star){
                rendstar();
                if(newSize1>0.0012) speed_s=0.1f;
            }

            if(rend.material.shader == shader_moln){
                if(sats==2f || sats==1f || sats==3f){
                    rendmoln();
                }
                moln();
            }

            if(sats==1f || sats==3f){
                m=0;
                speed_s=1f;
            }

            if(newSize1<0.00009 && rend.material.shader == shader_star && m==0f){
                rend.material.shader = shader_back;
                rend.material.SetColor("_Color", color3);
                rend.material.SetColor("_Color1", color3);
                rend.material.SetFloat("_transparency", 1);
            }

            if(sats==1f){
                back2();
            }else if(sats==3f){
                back3();
            }else if(sats==4f){
                if(rend.material.shader == shader_moln){
                    changefire();
                }
                if(rend.material.shader == shader_eld){
                    rendfire();
                }
            }

            if(rend.material.shader == shader_back){
                rendback();
            }
    }

    void rendfire(){
        fire_g=Mathf.Lerp(rend.material.GetFloat("_t"), fire, 0.09f*Time.deltaTime);
        rend.material.SetFloat("_t", fire_g);
    }

    void changefire(){

        if(rend.material.GetFloat("_t")>0.00001 ){
            trans=0f;
            newsize2=Mathf.Lerp(rend.material.GetFloat("_t"), trans,Time.deltaTime);
            rend.material.SetFloat("_t", newsize2);
        }else{
            rend.material.shader = shader_eld;
            rend.material.SetColor("_Color", new Vector4(1f,0.7f,0.2f,1f));
            rend.material.SetColor("_Color1", new Vector4(0.4f,0.2f,0.2f,0.5f));
            rend.material.SetColor("_Color2", new Vector4(1f,0.3f,0.3f,0.5f));
            rend.material.SetFloat("_b", 1.9f);
            rend.material.SetFloat("_d", 0.86f);
            rend.material.SetFloat("_Height", 0.3f);
            rend.material.SetFloat("_zoom", 6.2f);
            rend.material.SetFloat("_t", 0f);
            rend.material.SetFloat("_c", 0f);
            rend.material.SetFloat("_a", 0.133f);
            fire=1f;
        }
    }

    public void sats1(){
        sats=1f;
        color_c=color_w;
        trans=1f;
        time1=0f;
    }

    public void sats2(){
        sats=3f;
        n=1f;
    }

    public void sats3(){
        sats=4f;
        time2=0f;
        trans=0f;
        color2=color_b;
        color1=color_b;
        speed1=0.5f*speed;
    }

    void back3(){

        if(time2>=0f && time2<=1f){
            color1=new Vector4(0.58f,0.47f,1f,1f);
            color2=new Vector4(0.37f,0.46f,0.89f,0.5f);
            speed=0.9f;
        }else if(time2>=100 && time2<101f){
            color2= new Vector4(0.69f,0.22f,0.15f,0.5f);
            speed1=speed;
        }else if(time2>=140 && time2<141f){
            color1= new Vector4(0.783f,0.336f,0.137f,1.0f);
        }
        time2+=0.5f;
    }

    void rendback(){
        color3 = Vector4.Lerp(rend.material.GetColor("_Color"), color1, speed1* Time.deltaTime);
        color4 = Vector4.Lerp(rend.material.GetColor("_Color1"), color2, speed * Time.deltaTime);

        rend.material.SetColor("_Color", color3);
        rend.material.SetColor("_Color1", color4);
    }

    void rendmoln(){
        newsize2=Mathf.Lerp(rend.material.GetFloat("_t"), trans, speed_c*Time.deltaTime);
        rend.material.SetFloat("_t", newsize2);

        color=Vector4.Lerp(rend.material.GetColor("_Color"), color_c, 0.9f*Time.deltaTime);
        rend.material.SetColor("_Color", color);
    }

    void rendstar(){
        rend.material.SetFloat("_c", newSize1);
        newSize1 = Mathf.Lerp(rend.material.GetFloat("_c"), m, speed_s*Time.deltaTime);
    }

    void back2(){
        if(time1>=0f && time1<1f){
            color1=new Vector4(0.58f,0.47f,0.77f,1f);
            color2=new Vector4(0.37f,0.46f,0.89f,0.5f);
            speed1=0.09f;
        }if(time1>=400 && time1<401){
            color1=new Vector4(0.47f,0.59f,1f,1f);
            speed1=speed*0.5f;
        }
        time1+=0.5f;
    }

    void moln(){

        if(n==1){
            time_c+=0.1f;

            if(time_c>20f){
                trans=0f;
                speed_c=2f;
            }

        }else{
            if(rend.material.GetFloat("_t")>0.04f){
                speed_c=0.02f;
            }
            if(rend.material.GetFloat("_t")>0.4f){
                speed_c=0.1f;
            }
        }
        if(color.x<0.5f){
            trans=1f;
            b_2=Mathf.Lerp(rend.material.GetFloat("_d"), 4f, Time.deltaTime);
            rend.material.SetFloat("_d",b_2);
        }
    }
}
