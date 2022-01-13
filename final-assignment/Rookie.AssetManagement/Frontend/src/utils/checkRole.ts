import Roles from "../constants/roles";

export const checkUserIsAdmin = currentUser => {
    if (!currentUser || !currentUser.Role) return false;
    if (currentUser.Role = Roles.Admin) return true;
    return false;
  }