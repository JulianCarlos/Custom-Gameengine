#version 330 core

in vec3 FragPos;
in vec3 Normal;
in vec2 TexCoord;

uniform vec3 lightDirection;
uniform vec3 lightColor;
uniform vec3 ambientColor;
uniform vec3 diffuseColor;
uniform vec3 specularColor;
uniform float shininess;

uniform sampler2D tex;

out vec4 FragColor;

void main()
{
    vec3 lightDir = normalize(lightDirection);
    float diffuseFactor = max(abs(dot(lightDir, normalize(Normal))), 0);
    vec3 diffuse = diffuseFactor * lightColor * diffuseColor;

    vec3 viewDirection = normalize(-FragPos);
    vec3 reflectDirection = reflect(lightDir, normalize(Normal));
    float specularFactor = pow(max(dot(reflectDirection, viewDirection), 0), shininess);
    vec3 specular = specularColor * specularFactor;

    vec3 ambient = ambientColor;
    vec3 texel = texture(tex, TexCoord).rgb;

    FragColor = vec4(diffuse * texel + ambient + specular, 1);
}