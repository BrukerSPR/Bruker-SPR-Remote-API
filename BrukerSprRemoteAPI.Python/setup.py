from setuptools import setup, find_packages
import os

readme_path = os.path.join(os.path.dirname(__file__), "README.md")
long_description = open(readme_path, encoding="utf-8").read() if os.path.exists(readme_path) else ""

setup(
    name="BrukerSprRemoteAPI",
    version="1.0.0",
    description="Python client library for Bruker SPR Remote API",
    long_description=long_description,
    long_description_content_type="text/markdown",
    author="Bruker",
    packages=find_packages(),
    python_requires=">=3.7",
    classifiers=[
        "Programming Language :: Python :: 3",
        "Operating System :: OS Independent",
    ],
)
