SCRIPT_SRC = update-debian

SCRIPT_NAME = update-debian

INSTALL_DIR = /usr/bin

.PHONY: install uninstall

make:
	@echo "Available commands:\ninstall\nuninstall"

install:
	@# Check if user is root
	@if [ "$(shell id -u)" != "0" ]; then \
		echo "Error: This must be run as root. Try 'sudo make install'." >&2; \
		exit 1; \
	fi
	
	@echo "Installing $(SCRIPT_SRC) to $(INSTALL_DIR)/$(SCRIPT_NAME)..."
	@# Install the script and set permissions to executable (755)
	@install -m 755 $(SCRIPT_SRC) $(INSTALL_DIR)/$(SCRIPT_NAME)
	@echo "Install complete."

uninstall:
	@# Check if user is root
	@if [ "$(shell id -u)" != "0" ]; then \
		echo "Error: This must be run as root. Try 'sudo make uninstall'." >&2; \
		exit 1; \
	fi
	
	@echo "Removing $(INSTALL_DIR)/$(SCRIPT_NAME)..."
	@# Remove the installed script
	@rm -f $(INSTALL_DIR)/$(SCRIPT_NAME)
	@echo "Uninstall complete."