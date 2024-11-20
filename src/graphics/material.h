#pragma once

#include <glm/vec3.hpp>
#include <glm/vec4.hpp>
#include <glm/matrix.hpp>


#include "openvdbReader.h"
#include "bbox.h"

#include "../framework/camera.h"
#include "mesh.h"
#include "texture.h"
#include "shader.h"

class Material {
public:

	Shader* shader = NULL;
	Texture* texture = NULL;
	glm::vec4 color;

	virtual void setUniforms(Camera* camera, glm::mat4 model) = 0;
	virtual void render(Mesh* mesh, glm::mat4 model, Camera* camera) = 0;
	virtual void renderInMenu() = 0;
};

class FlatMaterial : public Material {
public:

	FlatMaterial(glm::vec4 color = glm::vec4(1.f));
	~FlatMaterial();

	void setUniforms(Camera* camera, glm::mat4 model);
	void render(Mesh* mesh, glm::mat4 model, Camera* camera);
	void renderInMenu();
};

class WireframeMaterial : public FlatMaterial {
public:

	WireframeMaterial();
	~WireframeMaterial();

	void render(Mesh* mesh, glm::mat4 model, Camera* camera);
};

class StandardMaterial : public Material {
public:

	bool first_pass = false;

	bool show_normals = false;
	Shader* base_shader = NULL;
	Shader* normal_shader = NULL;

	StandardMaterial(glm::vec4 color = glm::vec4(1.f));
	~StandardMaterial();

	void setUniforms(Camera* camera, glm::mat4 model);
	void render(Mesh* mesh, glm::mat4 model, Camera* camera);
	void renderInMenu();
};
//EJERICIO 1 
class VolumeMaterial : public Material {
public:
	//Inicializamos todas las variables que necesitaremos para nuestro volumenMaterial
	glm::vec4 color_volum;
	float absorptionCoefficient = 1.0f;
	bool show_volum = false;
	bool show_hetereo = false;
	bool show_emision = false;
	float steps_num = 10.0f;
	float scale_noise = 10.5f;
	float noise_detail = 1.5f;
	glm::vec3 emisionColor;
	float emisionIntensity = 1.5f;
	Shader* Volum_shader = NULL;
	Shader* Base_shader = NULL;
	Shader* Hetereo_shader = NULL;
	Shader* Emision_shader = NULL;

	int densitySource = 0;
	float densityScale = 1.0f;

	VolumeMaterial(glm::vec4 color = glm::vec4(1.f));
	~VolumeMaterial();
	void setUniforms(Camera* camera, glm::mat4 model) override;
	void render(Mesh* mesh, glm::mat4 model, Camera* camera) override;
	void renderInMenu() override;

	void loadVDB(std::string file_path);
	void estimate3DTexture(easyVDB::OpenVDBReader* vdbReader);
};