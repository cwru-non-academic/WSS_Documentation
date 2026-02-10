Usage
=====

Prerequisites
-------------

- Python >= 3.9
- A ``Cs_Libraries/`` directory containing the required ``.dll`` files

DLL discovery walks upward from the package directory looking for ``Cs_Libraries/``.

Install (dev)
-------------

.. code-block:: bash

   python3 -m venv .venv
   source .venv/bin/activate
   python -m pip install -U pip
   python -m pip install -e .

Documentation (Sphinx)
----------------------

If you want to generate project documentation, install the optional ``docs`` extra
(this installs Sphinx):

.. code-block:: bash

   python3 -m venv .venv
   source .venv/bin/activate
   python -m pip install -U pip
   python -m pip install -e ".[docs]"

Then build the HTML docs:

.. code-block:: bash

   sphinx-build -b html docs docs/_build/html

CLI
---

.. code-block:: bash

   python -m wss_py_wrapper.cli -- --help
   python -m wss_py_wrapper.cli --

Simulated transport mode (still requires DLLs):

.. code-block:: bash

   python -m wss_py_wrapper.cli -- --test

Python API
----------

.. code-block:: python

   from pathlib import Path
   from wss_py_wrapper import StimulationController, WssConfig

   config = WssConfig.default(Path(__file__))
   controller = StimulationController(config)
   controller.Initialize()

   controller.StartStimulation()
   controller.StimWithMode("index", 0.25)
   controller.StopStimulation()

   controller.Shutdown()
