#version 330 core

in vec3 Normal;
in vec3 FragPos;

uniform vec3 viewPos;
uniform vec3 lightPos;
uniform vec3 objectColor;
uniform float outlineWidth;

out vec4 color;

void main()
{
    vec3 normal = normalize(Normal);
    vec3 lightDir = normalize(lightPos - FragPos);
    vec3 viewDir = normalize(viewPos - FragPos);

    float diffuse = max(dot(lightDir, normal), 0.0);
    vec3 reflectDir = reflect(-lightDir, normal);
    float specular = pow(max(dot(viewDir, reflectDir), 0.0), 32.0);
    vec3 diffuseColor = diffuse * objectColor;
    vec3 specularColor = vec3(1, 1, 1) * specular;

    float distance = length(FragPos - lightPos);
    float edge = outlineWidth / distance;

    if (diffuse > edge)
    {
        color = vec4(diffuseColor + specularColor, 1.0);
    }
    else
    {
        color = vec4(vec3(1, 1, 1), 1.0);
    }
}