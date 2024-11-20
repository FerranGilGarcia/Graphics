#pragma once

#include "framework/includes.h"
#include "framework/camera.h"
#include "framework/scenenode.h"
#include "framework/light.h"

#include "openvdbReader.h"
#include "bbox.h"

#include <glm/vec2.hpp>

class Application
{
public:
	static Application* instance;

	static Camera* camera;
	std::vector<SceneNode*> node_list;
	glm::vec4 ambient_light;
	glm::vec4 backgroundColor;
	std::vector<Light*> light_list;
	glm::vec3 boxMin;
	glm::vec3 boxMax;

	int window_width;
	int window_height;

	bool flag_grid;
	bool flag_wireframe;

	bool close = false;
	bool dragging;
	glm::vec2 mousePosition;
	glm::vec2 lastMousePosition;

	void init(GLFWwindow* window);
	void update(float dt);
	void render();
	void renderGUI();
	void shutdown();

	void onKeyDown(int key, int scancode);
	void onKeyUp(int key, int scancode);
	void onRightMouseDown();
	void onRightMouseUp();
	void onLeftMouseDown();
	void onLeftMouseUp();
	void onMiddleMouseDown();
	void onMiddleMouseUp();
	void onMousePosition(double xpos, double ypos);
	void onScroll(double xOffset, double yOffset);
};