const ServiceUtils = {
    onInvalidId(caller, onError) {
        const errorInfo = {
            status: 400,
            message: `[${caller}] A valid ID is required.`,
        };
        console.error(errorInfo.message);
        if (typeof onError === 'function') onError(errorInfo);
    }
};